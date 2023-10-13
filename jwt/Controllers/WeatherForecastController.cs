using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace jwt.Controllers
{
    /*[Authorize(Roles ="Admin")]*/
    [ApiController]
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IProductService _productService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,ApplicationDbContext applicationDbContext,IProductService productService)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _productService = productService;
        }

    //     [HttpGet]
    //     public IEnumerable<WeatherForecast> Get()
    //     {
         
    //         return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //         {
    //             Date = DateTime.Now.AddDays(index),
    //             TemperatureC = Random.Shared.Next(-20, 55),
    //             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //         })
    //         .ToArray();
    //     }
    //     [HttpGet]
    //     public async Task<IActionResult> tryHer(int id,int unitId)
    //     {
    //             var income =await _applicationDbContext.InvoiceDetails.Where(a=>a.ProductId==id&&a.isEntering==true).GroupBy(a => true).Select(a =>
    //        new {
               
    //           quantityIn =(decimal) a.Sum(y => y.Quantity*y.ProductUnit.Ratio),
    //            costIn =(decimal)  a.Sum(y => (y.CostIn / y.ProductUnit.Ratio)*(y.ProductUnit.Ratio*y.Quantity)),

    //        }).FirstOrDefaultAsync();
    //        var outcome =await _applicationDbContext.InvoiceDetails.Where(a => a.ProductId == id && a.isEntering == false).GroupBy(a => true).Select(a =>
    //   new {

    //       quantityOut =(decimal) a.Sum(y => y.Quantity * y.ProductUnit.Ratio),
    //       costOut =(decimal)  a.Sum(y => (y.CostOut/ y.ProductUnit.Ratio)*(y.ProductUnit.Ratio*y.Quantity)),



    //   }).FirstOrDefaultAsync();

    //         decimal cost = 0;
    //         decimal quantity = 0;
    //         if(income is not null)
    //         {
    //             quantity = income.quantityIn;
    //             cost = income.costIn;
    //             if(outcome is not null)
    //             {
    //                 quantity = quantity - outcome.quantityOut;
    //                 cost = cost - outcome.costOut;

    //             }
    //         }

    //         if (quantity != 0)
    //         {
    //             cost = cost / quantity;
    //         }
    //         var product = await _productService.GetProductByIdAsync(id);
            
    //         var a = new
    //         {
    //             costdefualtunit = cost,
    //             quantityAvailable = quantity,
    //             income=income,
    //             outcome=outcome,
    //             thisunitcost = cost * product.ProductUnits.FirstOrDefault(a => a.UnitId == unitId).Ratio,
    //             thisunitquantity=quantity/product.ProductUnits.FirstOrDefault(a=>a.UnitId==unitId).Ratio
    //     };


    //         return Ok(a);
    //     }
    //     [HttpGet]
    //     public  async Task<IActionResult> tp()
        // {
        //     Type aa = typeof(AppPermissions);
        //     var l = Assembly.GetExecutingAssembly().GetTypes().Where(a => a.Namespace == aa.Namespace).ToList();
        //     for(int i=1;i<l.Count;i++)
        //     {
        //         foreach(var b in l[i].GetFields()) {
        //             System.Console.WriteLine(b.GetValue(null).ToString());
        //         }
                
        //     }


            /*var fields = pages.GetFields(BindingFlags.Static | BindingFlags.Public);*/
            // var fields = pages.Assembly;
            /*  foreach (FieldInfo field in fields)
              {
                  allPermissions.Add(new RolePermissionsViewModel { Type = "Permissions", Value = field.GetValue(null).ToString() });
              }*/
          //  System.Console.WriteLine(pages.GenericTypeArguments);
        //     return Ok();

        // }
        private readonly HttpClient _httpClient = new HttpClient();

[HttpGet, Route("DotNetCount")]
[AllowAnonymous]
public async Task<IActionResult> Getjson()
{
    // Suspends GetDotNetCount() to allow the caller (the web server)
    // to accept another request, rather than blocking on this one.
   var json = JsonConvert.SerializeObject(new{key="[{Id:1,Name:wesam},{Id:1,Name:wesam}]"},
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

    

    return Ok(json);
}
       [HttpGet]
       [AllowAnonymous]
    public async Task<IActionResult> AsyncTasks(int id) {
int threadid= Thread.CurrentThread.ManagedThreadId;
Console.WriteLine($"main thread is {threadid}");
     var a= a1();
          threadid= Thread.CurrentThread.ManagedThreadId;
Console.WriteLine($"main thread is {threadid}");
 //var a3=a2();
  for(int i=0;i<=100;i++){
     Console.WriteLine($"inside main{i}");
 }
 //await a;
    



      
     // var a=await result;
        return Ok($"{id} is completed ");
       }
 private async  Task<int> a2(){
    int threadid= Thread.CurrentThread.ManagedThreadId;
    Console.WriteLine($"a2 thread is {threadid}");
    await Task.Delay(1);
     threadid= Thread.CurrentThread.ManagedThreadId;
    Console.WriteLine($"a2 thread is {threadid}");
    
   for(int i=0;i<=10;i++){
     Console.WriteLine($"inside a2");
 }  

   return 1;
}


 private  async Task<int> a1(){
        int threadid= Thread.CurrentThread.ManagedThreadId;
Console.WriteLine($"a1 thread is {threadid}");
    var a=await a2();
  //    a;
        threadid= Thread.CurrentThread.ManagedThreadId;
Console.WriteLine($"a1 thread is {threadid}");
     for(int i=0;i<=2;i++){
     Console.WriteLine($"inside a1");
  //  
 }
 //await a;
  
    return 1;
}
       private async Task hi(int id){

  
Console.WriteLine($"inside hi {id} ");
          
         await   Task.Delay(1000);
          
      
        //  Task.Delay(1000).Wait();
        Console.WriteLine($"endHi {id} ");
        //return Task.FromResult;
       }
         private async Task<int> hi2(int id){

  Console.WriteLine($"inside endHi2 {id} ");
            
          
         await   Task.Delay(9000);
         Console.WriteLine("Before the errror");
          
           throw new InvalidOperationException("The toaster is on fire");
        
        Console.WriteLine($"endHi2 {id} ");
        return id;
       }
    }
}