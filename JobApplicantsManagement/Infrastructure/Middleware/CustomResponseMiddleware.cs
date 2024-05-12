using JobApplicantsManagement.Infrastructure.Exceptions;
using System.Net;

namespace JobApplicantsManagement.Infrastructure.Middleware
{
    public class CustomResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var response = context.Response;
            var originBody = response.Body;
            using var newBody = new MemoryStream();
            response.Body = newBody;

            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(
                    response, 
                    ex.Message,
                    HttpStatusCode.UnprocessableEntity, 
                    originBody, 
                    newBody);
            }
            catch(BadRequestException ex)
            {
                await HandleExceptionAsync(
                    response,
                    ex.Message,
                    HttpStatusCode.BadRequest,
                    originBody,
                    newBody);
            }
        }

        private async Task HandleExceptionAsync(
            HttpResponse response, 
            string message, 
            HttpStatusCode httpStatus, 
            Stream originBody, 
            MemoryStream newBody)
        {
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatus;

            var stream = response.Body;
            stream.SetLength(0);
            using var writer = new StreamWriter(stream, leaveOpen: true);
            await writer.WriteAsync(message);
            await writer.FlushAsync();

            response.ContentLength = stream.Length;

            newBody.Seek(0, SeekOrigin.Begin);
            await newBody.CopyToAsync(originBody);
            response.Body = originBody;
        }
    }
}
