using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Azure_Blob_Trigger
{
    public class AzureBlobTriggerFunction
    {
        private readonly ILogger _logger;

        private readonly StudentContext _context;

        public AzureBlobTriggerFunction(ILogger<AzureBlobTriggerFunction> logger, StudentContext context)
        {
            _logger = logger;
            _context = context;            
        }

        [Function(nameof(AzureBlobTriggerFunction))]
        public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "AzureWebJobsStorage")] Stream stream, string name)
        {

            _logger.LogInformation($" Blob Trigger function passed blob\n Name : {name} \n Size : {stream.Length} Bytes");

            try
            {

                var students = await JsonSerializer.DeserializeAsync<List<Student>>(stream);


                if(students != null)
                {
                    // Insert data into the database
                    await _context.Students.AddRangeAsync(students);

                    await _context.SaveChangesAsync();

                    _logger.LogWarning("Student Data is Inserted into the Database...");

                }

                else
                {
                    _logger.LogWarning("No Student data found in the table");
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while processing the blob : {ex.Message}");
            }


        }
    }
}
