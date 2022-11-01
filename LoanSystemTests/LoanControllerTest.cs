using LoanSystem.Controllers;
using LoanSystem.Models;
using LoanSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCase
{
    public class LoanControllerTest
    {
        [Fact]
        public void GetAllLoan_ReturnOk()
        {
            Loan loan = new Loan { LoanId = 20, LoanTypeId = 15, CustomerId = 5, LoanAmount = 200000, DateOfSanction = new DateTime(2021, 10, 15) };
            Loan loan2 = new Loan { LoanId = 20, LoanTypeId = 15, CustomerId = 5, LoanAmount = 200000, DateOfSanction = new DateTime(2021, 10, 15) };
            List<Loan> loans = new List<Loan>();
            loans.Add(loan);
            loans.Add(loan2);
            var mockservice = new Mock<ILoanRepository>();
            mockservice.Setup(x => x.GetLoans()).Returns(loans);
            var controller = new LoanController(mockservice.Object);
            var okResult = controller.GetAllLoans();
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void GetAllLoan_ReturnNotFound()
        {
            List<Loan> loan = new List<Loan>();
            var mockservice = new Mock<ILoanRepository>();
            mockservice.Setup(x => x.GetLoans()).Returns(loan);
            var controller = new LoanController(mockservice.Object);
            var okResult = Assert.Throws<Exception>(() => controller.GetAllLoans());
        }

        [Fact]
        public void GetByLoanId_ReturnOk()
        {
            LoanDetailDTO loan = new LoanDetailDTO { LoanId = 16, LoanTypeId = 16, CustomerId = 7, LoanAmount = 200000, DateOfSanction = new DateTime(2021, 9, 12) };
            List<LoanDetailDTO> loans = new List<LoanDetailDTO>();
            loans.Add(loan);
            var mockservice = new Mock<ILoanRepository>();
            mockservice.Setup(x => x.GetLoanById(It.IsAny<int>())).Returns(loan);
            var controller = new LoanController(mockservice.Object);
            var okResult = controller.GetByLoanId(16);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.StrictEqual(16, loan.LoanId);
        }

        [Fact]
        public void GetByLoanId_ReturnException()
        {
            LoanDetailDTO loan = null;
            var mockservice = new Mock<ILoanRepository>();
            mockservice.Setup(x => x.GetLoanById(It.IsAny<int>())).Returns(loan);
            var controller = new LoanController(mockservice.Object);
            var okResult = Assert.Throws<Exception>(() => controller.GetByLoanId(50));
            Assert.IsType<Exception>(okResult);
            Assert.NotEqual("The LoanId exists", okResult.Message);
        }

        [Fact]
        public void AddLoan_ReturnOk()
        {
            LoanDetailDTO loan = new LoanDetailDTO { LoanId = 20, LoanTypeId = 15, CustomerId = 5, LoanAmount = 200000, DateOfSanction = new DateTime(2021, 10, 15) };
            Messages message = new Messages();
            message.Success = true;
            message.Message = "Added Successfully";
            var mockservice = new Mock<ILoanRepository>();
            mockservice.Setup(x => x.AddLoan(loan)).Returns(message);
            var controller = new LoanController(mockservice.Object);
            var okResult = controller.AddLoan(loan);
            Assert.IsType<OkObjectResult>(okResult);
        }

    }
}