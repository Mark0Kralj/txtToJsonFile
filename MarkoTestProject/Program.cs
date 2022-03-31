using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using MarkoTestProject;


//File Path - you can change this to your file path 
string textFile = @"C:\POS\20210806.txt";
//Path to folder where you put your JSON files - you can change this to your path
string pathToFolder = @"C:\JSON\";


//header
int indexStartOrderNumber  = 2;
int lengthOrderNumber = 9;
int indexCompanyCode = 11;
int lenghtCompanyCode = 7;
int indexOrderDate = 18;
int lenghtOrderDate = 13;
int indexPosNR = 31;
int lenghtPosNR = 5;
int indexAddress = 36;
int lenghtAddress = 25;
int indexPhone = 61;
int lenghtPhone = 15;

//raws
int indexArticleNumber = 2;
int lenghtArticleNumber = 9;
int indexSize = 11;
int lenghtSize = 5;
int indexQuantity = 16;
int lenghtQuantity = 6;
int indexPriceIncVat = 22;
int lenghtPriceIncVat = 11;
int indexPriceExVat = 33;
int lenghtPriceExVat = 11;
int indexColor = 44;
int lenghtColor = 17;
int indexReference = 61;
int lenghtReference = 30;

string  jsonString= "";
string filePathCreation = "";
string orderJsonFileName = "";
int lineCount =0;
int numberOfOrders = 0;

//Read all Lines and count number of Orders
string [] lines = File.ReadAllLines(textFile);

foreach (string lineNumber in lines)
{
    if (lineNumber.StartsWith("E") || lineNumber.StartsWith("e"))
    {
        numberOfOrders++;
    }
}

Console.WriteLine("Number of orders in "+textFile+" file is : "+numberOfOrders);


TextWriter writer;



//Creat orders,write orders in JSON files
for (var i = 0; i < numberOfOrders; i++)
{
    string[] orderLines =File.ReadLines(textFile).Skip(lineCount).ToArray();

    Order newOrder = new Order();

    foreach (string orderline in orderLines)
    {
        lineCount++;

        if(orderline.StartsWith("E") || orderline.StartsWith("e"))
        {
            break;
        }
        else if(orderline.StartsWith("H") || orderline.StartsWith("h"))
        {
            //grab header
            newOrder.OrderNumber = orderline.Substring(indexStartOrderNumber, lengthOrderNumber);
            newOrder.CompanyCode = orderline.Substring(indexCompanyCode, lenghtCompanyCode);
            newOrder.OrderDate = orderline.Substring(indexOrderDate, lenghtOrderDate);
            newOrder.PosNumber = orderline.Substring(indexPosNR, lenghtPosNR);
            newOrder.Address = orderline.Substring(indexAddress, lenghtAddress);
            newOrder.Phone = orderline.Substring(indexPhone, lenghtPhone);
            newOrder.Raws = new List<Raw> { };
            orderJsonFileName = "OrderNumber_"+newOrder.OrderNumber +".json";
            filePathCreation = Path.Combine(pathToFolder, orderJsonFileName);

            //Check if i get staff back.
            //Console.WriteLine("Order Number: " + newOrder.OrderNumber + " CompanyCode: " + newOrder.CompanyCode + " Order Date" + newOrder.OrderDate + "PosNR: " + newOrder.PosNumber + " Address:" + newOrder.Address + " Phone: " + newOrder.Phone);
        }
        else if(orderline.StartsWith("R") || orderline.StartsWith("r"))
        {
            Raw newRaw = new Raw();

            //grab raw
            newRaw.ArticleNumber = orderline.Substring(indexArticleNumber, lenghtArticleNumber);
            newRaw.Size = orderline.Substring(indexSize, lenghtSize);
            newRaw.Quantity = orderline.Substring(indexQuantity, lenghtQuantity);
            newRaw.PriceIncVat = orderline.Substring(indexPriceIncVat, lenghtPriceIncVat);
            newRaw.PriceExVat = orderline.Substring(indexPriceExVat, lenghtPriceExVat);
            newRaw.Color = orderline.Substring(indexColor, lenghtColor);
            newRaw.Reference = orderline.Substring(indexReference, lenghtReference);
            newOrder.Raws.Add(newRaw);

            //Check if i get staff back.
            //Console.WriteLine("Article Number: " + ArticaleNumber + " Size: " + Size + " Quantity: " + Quantity + " Price Inc Vat: " + PriceIncVat + " Price Ex Vat: " + PriceExVat + " Color: " + Color + " Reference: " + Reference);
        }
    }
    


    //Convert TXT to JSON
    jsonString = JsonSerializer.Serialize(newOrder);

    //Creat JSON file
    FileStream fs = File.Create(filePathCreation);
    Console.WriteLine("JSON file "+orderJsonFileName+" was created in " + pathToFolder);
    fs.Close();

    //Write to JSON file
    writer = new StreamWriter(filePathCreation, true);
    writer.WriteLine(jsonString);
    writer.Close();

    //Check line count 
    //Console.WriteLine("Line count after while loop: " + lineCount);
    

}




