using System.Net;
using BusinessLayer.Dtos;
using TranslationManagement.Api.Tests.Utils;

namespace TranslationManagement.Api.Tests;

public class TranslationJobTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _client;

    public TranslationJobTests(CustomWebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetJobs_ReturnsOk()
    {
        // Act
        var response = await _client.GetFromJsonAsync<List<TranslationJobDto>>("/api/jobs/GetJobs");

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response);
        // assert that one has customer name Alice that has been seeded
        Assert.Contains(response, job => job.CustomerName == "Alice");

    }

    [Fact]
    public async Task CreateJob_ReturnsOk()
    {
        // Arrange
        var translationJob = new CreateTranslationJobDto
        {
            CustomerName = "Test",
            OriginalContent = "Test content"
        };

        var content = JsonContent.Create(translationJob);

        // Act
        var response = await _client.PostAsync("/api/jobs/CreateJob", content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateJob_ReturnsBadRequest()
    {
        // Arrange
        var translationJob = new CreateTranslationJobDto
        {
            CustomerName = "Test",
            OriginalContent = null
        };

        var content = JsonContent.Create(translationJob);

        // Act
        var response = await _client.PostAsync("/api/jobs/CreateJob", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // created job will be returned from get all jobs
    [Fact]
    public async Task GetJobs_ReturnsCreatedOne()
    {
        // Arrange
        var translationJob = new CreateTranslationJobDto
        {
            CustomerName = "Test",
            OriginalContent = "Test content"
        };

        var content = JsonContent.Create(translationJob);

        // Act
        var response = await _client.PostAsync("/api/jobs/CreateJob", content);
        response.EnsureSuccessStatusCode();

        var jobs = await _client.GetFromJsonAsync<List<TranslationJobDto>>("/api/jobs/GetJobs");

        // Assert
        Assert.NotNull(jobs);
        Assert.Contains(jobs, job => job.CustomerName == "Test");
    }

    // created a job and update its status
    [Fact]
    public async Task UpdateJobStatus_UpdatesStatusSuccessfully()
    {
        // Step 1: Get all jobs to find a job to update
        var jobs = await _client.GetFromJsonAsync<List<TranslationJobDto>>("/api/jobs/GetJobs");

        Assert.NotNull(jobs);
        Assert.NotEmpty(jobs);

        // Get the first job
        var job = jobs[0];
        var initialStatus = job.Status;

        // Step 2: Update the status of the first job
        var newStatus = initialStatus == "New" ? "InProgress" : "Completed";
        var updateResponse =
            await _client.PutAsync($"/api/jobs/UpdateJobStatus?jobId={job.Id}&translatorId=1&newStatus={newStatus}",
                null);

        // Assert update was successful
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        // Step 3: Get all jobs again and verify the job status has changed
        var updatedJobs = await _client.GetFromJsonAsync<List<TranslationJobDto>>("/api/jobs/GetJobs");
        Assert.NotNull(updatedJobs);
        var updatedJob = updatedJobs.Find(j => j.Id == job.Id);

        // Assert the job status was updated
        Assert.NotNull(updatedJob);
        Assert.Equal(newStatus, updatedJob.Status);
    }
}
