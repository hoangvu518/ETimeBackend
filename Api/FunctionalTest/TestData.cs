using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FunctionalTest
{
    public class TestData
    {

        public static TheoryData<CreateEmployeeDto> BadCreateEmployeeDto =>
            new TheoryData<CreateEmployeeDto>
            {
                new CreateEmployeeDto { Email = "", FirstName = "Hoang", LastName = "Pham", ManagerId = null, Salary = 10000 } ,
                new CreateEmployeeDto { Email = "hoang.com", FirstName = "Hoang", LastName = "Pham", ManagerId = null, Salary = 10000 },
                new CreateEmployeeDto { Email = "Hoang@gmail.com", FirstName = "", LastName = "Pham", ManagerId = null, Salary = 10000 },
                new CreateEmployeeDto { Email = "Hoang@gmail.com", FirstName = "11111111111111111111111111111111111111111111111111111111111111111111111111", LastName = "Pham", ManagerId = null, Salary = 10000 },
                new CreateEmployeeDto { Email = "Hoang@gmail.com", FirstName = "Hoang", LastName = "", ManagerId = null, Salary = 10000 },
                new CreateEmployeeDto { Email = "Hoang@gmail.com", FirstName = "Hoang", LastName = "11111111111111111111111111111111111111111111111111111111111111111", ManagerId = null, Salary = 10000 },
                new CreateEmployeeDto { Email = "Hoang@gmail.com", FirstName = "Hoang", LastName = "", ManagerId = null, Salary = 10000000 },
            };
    };

}
