//using LoanSystem.Controllers;
//using LoanSystem.Repository;
//using Microsoft.AspNetCore.Localization;
//using Microsoft.AspNetCore.Mvc;
//using LoanSystem.Models;
//using Moq;

//namespace TestCase
//{
//    public class CustomerControllerTest
//    {
//        [Fact]
//        public void GetAllCustomer_ReturnOk()
//        {
//            Customer customer = new Customer { CustomerId = 3, CustomerName = "Vijaya", MobileNumber = "9441009834", AadharNumber = 652221900034 };
//            Customer customer2 = new Customer { CustomerId = 5, CustomerName = "Renuka", MobileNumber = "8766453534", AadharNumber = 675556219870 };
//            List<Customer> customers = new List<Customer>();
//            customers.Add(customer);
//            customers.Add(customer2);
//            var mockservice = new Mock<ICustomerRepository>();
//            mockservice.Setup(x => x.GetAll()).Returns(customers);
//            var controller = new CustomerController(mockservice.Object);
//            var okResult = controller.GetAll();
//            Assert.IsType<OkObjectResult>(okResult);
//        }

//        [Fact]
//        public void GetAllCustomer_ReturnException()
//        {
//            List<Customer> customers = new List<Customer>();
//            var mockservice = new Mock<ICustomerRepository>();
//            mockservice.Setup(x => x.GetAll()).Returns(customers);
//            var controller = new CustomerController(mockservice.Object);
//            var result = Assert.Throws<Exception>(() => controller.GetAll());
//            Assert.IsType<Exception>(result);
//            Assert.Equal("No Details in the customer table", result.Message);
//        }

//        [Fact]
//        public void GetByCustomerId_ReturnOK()
//        {
//            Customer customer = new Customer { CustomerId = 3, CustomerName = "Vijaya", MobileNumber = "9441009834", AadharNumber = 652221900034 };
//            Customer customer2 = new Customer { CustomerId = 5, CustomerName = "Renuka", MobileNumber = "8766453534", AadharNumber = 675556219870 };
//            var mockservice = new Mock<ICustomerRepository>();
//            mockservice.Setup(x => x.GetById(It.IsAny<int>())).Returns(customer);
//            var controller = new CustomerController(mockservice.Object);
//            var okResult = controller.GetById(3) as OkObjectResult;
//            Assert.NotNull(okResult);
//            var model = okResult.Value as Customer;
//            Assert.IsType<OkObjectResult>(okResult);
//            Assert.StrictEqual(customer.CustomerId, model.CustomerId);
//        }

//        [Fact]
//        public void GetByCustomerId_ReturnNotFound()
//        {
//            Customer customer = null;
//            var mockservice = new Mock<ICustomerRepository>();
//            mockservice.Setup(x => x.GetById(It.IsAny<int>())).Returns(customer);
//            var controller = new CustomerController(mockservice.Object);
//            var result = controller.GetById(30);
//            Assert.IsType<NotFoundResult>(result);
//        }

//        [Fact]
//        public void GetByCustomerName_ReturnOK()
//        {
//            Customer customer = new Customer { CustomerId = 3, CustomerName = "Vijaya", MobileNumber = "9441009834", AadharNumber = 652221900034 };
//            Customer customer2 = new Customer { CustomerId = 5, CustomerName = "Renuka", MobileNumber = "8766453534", AadharNumber = 675556219870 };
//            var mockservice = new Mock<ICustomerRepository>();
//            mockservice.Setup(x => x.GetByName(It.IsAny<string>())).Returns(customer);
//            var controller = new CustomerController(mockservice.Object);
//            var okResult = controller.GetByName("Vijaya") as OkObjectResult;
//            Assert.NotNull(okResult);
//            var model = okResult.Value as Customer;
//            Assert.IsType<OkObjectResult>(okResult);
//            Assert.Equal(customer.CustomerName, model.CustomerName);
//        }

//        [Fact]
//        public void GetByCustomerName_ReturnException()
//        {
//            Customer customer = null;
//            var mockservice = new Mock<ICustomerRepository>();
//            mockservice.Setup(x => x.GetByName(It.IsAny<string>())).Returns(customer);
//            var controller = new CustomerController(mockservice.Object);
//            var okResult = Assert.Throws<Exception>(() => controller.GetByName("Dharani"));
//            Assert.IsType<Exception>(okResult);
//            Assert.NotEqual("Id not found", okResult.Message);
//            Assert.Equal("CustomerName not found in the list", okResult.Message);
//        }

//        [Fact]
//        public void AddCustomer_ReturnOk()
//        {
//            Messages message = new Messages();
//            message.Success = true;
//            message.Message = "Added Successfully";
//            CustomerDTO customer = new CustomerDTO { CustomerId = 11, CustomerName = "Devika", MobileNumber = "9800332212", AadharNumber = 645398123002 };
//            var mockservice = new Mock<ICustomerRepository>();
//            mockservice.Setup(x => x.AddCustomer(It.IsAny<CustomerDTO>())).Returns(message);
//            var controller = new CustomerController(mockservice.Object);
//            var result = controller.AddCustomer(customer);
//            Assert.IsType<OkObjectResult>(result);
//            Assert.Matches("^([0-9]{10})$", customer.MobileNumber);
//            Assert.DoesNotMatch("^([1-9]{10})$", customer.MobileNumber);
//        }

//        [Fact]
//        public void UpdateCustomer_Return()
//        {
//            Messages message = new Messages();
//            message.Success = true;
//            message.Message = "updated Successfully";
//            CustomerDTO customer = new CustomerDTO { CustomerId = 1, CustomerName = "Priya", MobileNumber = "9876654342", AadharNumber = 984556754320 };
//            var mockservice = new Mock<ICustomerRepository>();
//            mockservice.Setup(x => x.UpdateCustomer(It.IsAny<CustomerDTO>())).Returns(message);
//            var controller = new CustomerController(mockservice.Object);
//            var result = controller.UpdateCustomer(customer) ;
//            Assert.IsType<OkObjectResult>(result);
//            //var model = result.Value as Customer;
//            //Assert.Equal(model.CustomerId, customer.CustomerId);
//        }

//        [Fact]
//        public void UpdateCustomer_ReturnException()
//        {
//            Messages message = new Messages();
//            message.Success = false;
//            message.Message = "not updated";
//            CustomerDTO customer = new CustomerDTO();
//            var mockservice = new Mock<ICustomerRepository>();
//            mockservice.Setup(x => x.UpdateCustomer(It.IsAny<CustomerDTO>())).Returns(message);
//            var controller = new CustomerController(mockservice.Object);

//            var result = Assert.Throws<Exception>(() => controller.UpdateCustomer(customer));
//            Assert.IsType<Exception>(result);
//            Assert.Equal("The customerid not exists", result.Message);
//        }

//        [Fact]
//        public void DeleteCustomer_ReturnOk()
//        {
//            Messages message = new Messages();
//            message.Success = true;
//            message.Message = "deleted";
//            Customer customer = new Customer { CustomerId = 1, CustomerName = "Priya", MobileNumber = "9876654342", AadharNumber = 984556754320 };
//            var mockservice = new Mock<ICustomerRepository>();
//            mockservice.Setup(x => x.DeleteCustomer(It.IsAny<int>())).Returns(message);
//            var controller = new CustomerController(mockservice.Object);
//            var delete = controller.DeleteCustomer(1) as OkObjectResult;
//            Assert.IsType<OkObjectResult>(delete);
//            //var result = delete.Value as Customer;
//            //Assert.Equal(result.CustomerName, customer.CustomerName);
//        }

//    }
//}