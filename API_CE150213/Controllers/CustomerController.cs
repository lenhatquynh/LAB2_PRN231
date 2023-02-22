﻿using API_CE150213.Repository.CustomerRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Data_CE150213.Models;
namespace API_CE150213.Controllers;

[Route("odata/[controller]/[Action]")]
[ApiController]
public class CustomerController : ODataController
{
    private readonly ICustomerRepository CustomerRepository;

    public CustomerController(ICustomerRepository CustomerRepository)
    {
        this.CustomerRepository = CustomerRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(Customer customer)
    {
        var check = await CustomerRepository.AnyAsync(customer.Username);
        if (check)
        {
            return BadRequest("Customer is exist!");
        }
        var created = await CustomerRepository.CreateAsync(customer);
        if (created)
        {
            return Ok("Create Success!");
        }
        return BadRequest("Create fail!");
    }

    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await CustomerRepository.GetCustomersAsync();
        if (customers == null)
        {
            return NotFound();
        }
        return Ok(customers);
    }

    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> GetCustomerByUsername(string username)
    {
        var customer = await CustomerRepository.GetCustomerAsync(username);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    [EnableQuery]
    [HttpDelete]
    public async Task<IActionResult> DeleteCustomer(string username)
    {
        var check = await CustomerRepository.AnyAsync(username);
        if (!check)
        {
            return NotFound();
        }
        var customer = await CustomerRepository.GetCustomerAsync(username);
        await CustomerRepository.DeleteAsync(customer);
        return Ok("Delete Success!");
    }
}
