using DataAccessLayer.Enums;
using DataAccessLayer.Models;

namespace DataAccessLayer.Utils;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        // Ensure database is created and clean for seeding
        context.Database.EnsureCreated();

        if (context.TranslationJobs.Any() || context.Translators.Any()) return; // DB has been seeded

        // Seed TranslationJobs
        context.TranslationJobs.AddRange(
            new TranslationJob
            {
                CustomerName = "Alice",
                Status = JobStatus.New,
                OriginalContent = "Hello",
                TranslatedContent = "Bonjour",
                Price = 10.0
            },
            new TranslationJob
            {
                CustomerName = "Bob",
                Status = JobStatus.InProgress,
                OriginalContent = "Goodbye",
                TranslatedContent = "Au revoir",
                Price = 15.0
            }
        );

        // Seed Translators
        context.Translators.AddRange(
            new Translator
            {
                Name = "John Doe",
                HourlyRate = "20",
                Status = TranslatorStatus.Applicant,
                CreditCardNumber = "1234-5678-9101-1121"
            },
            new Translator
            {
                Name = "Jane Smith",
                HourlyRate = "25",
                Status = TranslatorStatus.Applicant,
                CreditCardNumber = "1234-5678-9101-1131"
            }
        );

        // Save changes to the database
        context.SaveChanges();
    }
}
