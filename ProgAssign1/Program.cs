// See https://aka.ms/new-console-template for more information
using CsvHelper;
using CsvHelper.Configuration;
using DotNetAssignment;
using System.Diagnostics;
using System.Globalization;

int validRowCount = 0;
int invalidRowCount = 0;

Console.WriteLine("Enter Path To Data");
var dirPath = @"" + Console.ReadLine();


string logDir = String.Format(@"{0}\logs", Environment.CurrentDirectory);
if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);

if (!Directory.Exists(dirPath))
{
    using (AssignmentLogger logger = new AssignmentLogger(logDir))
    {
        logger.Log($"Parser did not run. {dirPath} is not a valid directory");
    }
    Console.WriteLine("Directory doesn't exist");
    return;
}

var readerConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    MissingFieldFound = null
};

string outputDir = String.Format(@"{0}\Output", Environment.CurrentDirectory);
if(!Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);

var stopwatch = Stopwatch.StartNew();
using (AssignmentLogger logger = new AssignmentLogger(logDir))
using (var writer = new StreamWriter(String.Format(@"{0}\Output\data.csv", Environment.CurrentDirectory)))
using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csvWriter.WriteHeader<Customer>();
    csvWriter.NextRecord();

    //Runs on every file encountered by the walker
    Action<string> WorkWithFile = (string filePath) =>
    {
        if (!filePath.EndsWith(".csv")) return;
        logger.LogStartOfDir(filePath);
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, readerConfig))
        {
            try
            {
                foreach (Customer customer in csv.GetRecords<Customer>())
                {
                    if (customer == null) 
                    {
                        logger.LogInvalidRow(csv.Parser.RawRow, "Empty Row"); 
                        invalidRowCount++;
                        continue; 
                    }
                    if (!Validator.NoEmptyFields(customer))
                    {
                        logger.LogInvalidRow(csv.Parser.RawRow, "Empty Field");
                        invalidRowCount++;
                        continue;
                    }
                    if (!Validator.NoUnwantedSpecialCharacter(customer))
                    {
                        logger.LogInvalidRow(csv.Parser.RawRow, "Unallowed special character(s) present");
                        invalidRowCount++;
                        continue;
                    }
                    if (!Validator.IsValidEmail(customer.email))
                    {
                        logger.LogInvalidRow(csv.Parser.RawRow, "Invalid Email");
                        invalidRowCount++; 
                        continue;
                    }
                    if (!Validator.IsNumber(customer.streetNum))
                    {
                        logger.LogInvalidRow(csv.Parser.RawRow, "Street Number is not a number");
                        invalidRowCount++;
                        continue;
                    }
                    if (!Validator.IsNumber(customer.phoneNumber))
                    {
                        logger.LogInvalidRow(csv.Parser.RawRow, "Phone Number is not a number");
                        invalidRowCount++;
                        continue;
                    }

                    validRowCount++;
                    customer.date = string.Join('-', filePath.Split("\\").TakeLast(4).Take(3).Reverse());
                    csvWriter.WriteRecord(customer);
                    csvWriter.NextRecord();
                }
            }
            catch (CsvHelper.HeaderValidationException e)
            {
                logger.Log($"Incompatible CSV file.  \n{e.Message.Split("Headers:")[0]} \nFile Closed.");
            }
        }
        logger.LogSeparator();
    };
    Walker.walkThen(dirPath, WorkWithFile);
    stopwatch.Stop();
    logger.Log($"Valid Row Count: {validRowCount} \nInvalid Row Count: {invalidRowCount} \nRun Time: {stopwatch.Elapsed}");
    Console.WriteLine($"Execution Complete \nValid Row Count: {validRowCount} \nInvalid Row Count: {invalidRowCount} \nRun Time: {stopwatch.Elapsed} \nLog File: {logDir}\\logs.txt \nOutput File:{outputDir}\\data.csv");
}

return;