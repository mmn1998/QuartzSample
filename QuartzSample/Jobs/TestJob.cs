using Microsoft.Extensions.Logging;
using Quartz;
using QuartzSample.Services;
using System;
using System.Threading.Tasks;

namespace QuartzSample.Jobs;

internal class TestJob : IJob
{
    private readonly ILogger<TestJob> _logger;
    private readonly IFileService _fileService;

    public TestJob(ILogger<TestJob> logger, IFileService fileService)
    {
        _logger = logger;
        _fileService = fileService;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        string filePath = AppDomain.CurrentDomain.BaseDirectory + @"/file.txt";
        string text = "Testing Quartz";

        await _fileService.SaveTextsToFile(filePath, text);
    }
}
