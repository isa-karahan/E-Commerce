using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace Core.Middlewares.Logging
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        public LoggerMiddleware(RequestDelegate next,
                                                ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                      .CreateLogger<LoggerMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }
        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }
        private async Task LogRequest(HttpContext context)
        {
            await using var requestStream = _recyclableMemoryStreamManager.GetStream(); // get the stream
            await context.Request.Body.CopyToAsync(requestStream); // read body
            // log info
            _logger.LogInformation($"Http Request Information:{Environment.NewLine}\n" +
                                   $"Schema:{context.Request.Scheme}\n" +
                                   $"Host: {context.Request.Host}\n" +
                                   $"Path: {context.Request.Path}\n" +
                                   $"QueryString: {context.Request.QueryString}\n" +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}\n");
            context.Request.Body.Position = 0; // reset the body position
        }

        private async Task LogResponse(HttpContext context)
        {
            // read the stream from the beginnig of the body
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            
            await _next(context);
            
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            _logger.LogInformation($"Http Response Information:{Environment.NewLine}\n" +
                                   $"Schema:{context.Request.Scheme}\n" +
                                   $"Host: {context.Request.Host}\n" +
                                   $"Path: {context.Request.Path}\n" +
                                   $"QueryString: {context.Request.QueryString} \n" +
                                   $"Response Body: {text} \n");
            
            // write the body to the response
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
    }
}
