using Data_CE150213.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Data_CE150213.Controllers;

public class CustomerController : Controller
{
    private readonly HttpClientHandler ClientHandler;
    public CustomerController()
    {
        ClientHandler = new HttpClientHandler();
        ClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
    }

    public async Task<IActionResult> Edit(string Username, [Bind("Username,Password,Fullname,Gender,Birthday,Address")] Customer customer)
    {
        if (Username != customer.Username)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            HttpClient clientx = new HttpClient(ClientHandler);
            clientx.BaseAddress = new Uri("https://localhost:7111/odata/Customer/UpdateCustomer");
            var res = await clientx.PutAsJsonAsync(clientx.BaseAddress, customer);
            if (res.StatusCode == System.Net.HttpStatusCode.OK) return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        HttpClient client = new HttpClient(ClientHandler);
        client.BaseAddress = new Uri($"https://localhost:7111/odata/Customer/GetCustomerByUsername?Username={id}");
        var res = await client.GetStringAsync(client.BaseAddress);
        Customer customer = JsonConvert.DeserializeObject<Customer>(res);

        return View(customer);
    }
    // GET: Customers
    public IActionResult Index()
    {
        HttpClient client = new HttpClient(ClientHandler);
        client.BaseAddress = new Uri("https://localhost:7111/odata/Customer/GetAllCustomers");
        var res = client.GetStringAsync(client.BaseAddress).GetAwaiter().GetResult();
        List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(res);
        return View(customers);
    }

    // GET: Customer/Delete/LeNhatQuynh
    public IActionResult Delete(string id)
    {
        HttpClient client = new HttpClient(ClientHandler);
        client.BaseAddress = new Uri($"https://localhost:7111/odata/Customer/GetCustomerByUsername?Username={id}");
        var res = client.GetStringAsync(client.BaseAddress).GetAwaiter().GetResult();
        Customer customer = JsonConvert.DeserializeObject<Customer>(res);

        return View(customer);
    }

    // POST: Customer/Delete/LeNhatQuynh
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(string id)
    {
        HttpClient client = new HttpClient(ClientHandler);
        client.BaseAddress = new Uri($"https://localhost:7111/odata/Customer/DeleteCustomer?Username={id}");
        var res = client.DeleteAsync(client.BaseAddress).GetAwaiter().GetResult();
        return RedirectToAction(nameof(Index));
    }
}