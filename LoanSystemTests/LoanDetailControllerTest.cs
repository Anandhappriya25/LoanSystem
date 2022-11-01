using LoanSystem.Controllers;
using LoanSystem.Models;
using LoanSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NuGet.Frameworks;

namespace TestCase
{
    public class LoanDetailControllerTest
    {
        [Fact]
        public void GetAllLoanDetails_ReturnOk()
        {
            LoanDetails loanDetail = new LoanDetails { LoanDetailsId = 8, LoanId = 17, PaidAmount = 20000, BalanceAmount = 120000, BalanceDuration = 20, TotalPaidAmount = 80000 };
            LoanDetails loanDetail2 = new LoanDetails { LoanDetailsId = 8, LoanId = 17, PaidAmount = 20000, BalanceAmount = 120000, BalanceDuration = 20, TotalPaidAmount = 80000 };
            List<LoanDetails> loanDetails = new List<LoanDetails>();
            loanDetails.Add(loanDetail);
            loanDetails.Add(loanDetail2);
            var mockService = new Mock<ILoanDetailRepository>();
            mockService.Setup(x => x.GetAll()).Returns(loanDetails);
            var controller = new LoanDetailController(mockService.Object);
            var okresult = controller.GetAllLoanDetail();
            Assert.IsType<OkObjectResult>(okresult);
        }

        [Fact]
        public void GetAllLoanDetails_ReturnException()
        {
            List<LoanDetails> loanDetails = new List<LoanDetails>();
            var mockService = new Mock<ILoanDetailRepository>();
            mockService.Setup(x => x.GetAll()).Returns(loanDetails);
            var controller = new LoanDetailController(mockService.Object);
            var okResult = controller.GetAllLoanDetail();
            Assert.IsType<NotFoundResult>(okResult);
        }

        [Fact]
        public void GetLoanDetailById_ReturnOk()
        {
            LoanDetails loanDetails = new LoanDetails { LoanDetailsId = 8, LoanId = 17, PaidAmount = 20000, BalanceAmount = 120000, BalanceDuration = 20, TotalPaidAmount = 80000 };
            var mockService = new Mock<ILoanDetailRepository>();
            mockService.Setup(x => x.GetById(It.IsAny<int>())).Returns(loanDetails);
            var controller = new LoanDetailController(mockService.Object);
            var okresult = controller.GetLoanDetailById(8) as OkObjectResult;
            Assert.NotNull(okresult);
            var model = okresult.Value as LoanDetails;
            Assert.StrictEqual(loanDetails.LoanId, model.LoanId);
        }

        [Fact]
        public void GetLoanDetailById_ReturnException()
        {
            LoanDetails loanDetails = null;
            var mockService = new Mock<ILoanDetailRepository>();
            mockService.Setup(x => x.GetById(It.IsAny<int>())).Returns(loanDetails);
            var controller = new LoanDetailController(mockService.Object);
            var result = Assert.Throws<Exception>(() => controller.GetLoanDetailById(40));
            Assert.IsType<Exception>(result);
            Assert.Equal("LoanDetailId not found in lists ", result.Message);
        }



        [Fact]
        public void PayLoan_ReturnOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "Payed";
            LoanDetails loanDetails = new LoanDetails { LoanDetailsId = 8, LoanId = 17, PaidAmount = 20000, BalanceAmount = 160000, BalanceDuration = 22, TotalPaidAmount = 40000 };
            var mockService = new Mock<ILoanDetailRepository>();
            mockService.Setup(x => x.PayLoan(It.IsAny<LoanDetails>())).Returns(messages);
            var controller = new LoanDetailController(mockService.Object);
            var okResult = controller.PayLoan(loanDetails);
            Assert.IsType<OkObjectResult>(okResult);
        }


        [Fact]
        public void ClosedLoanDetail_ReturnOk()
        {
            LoanDetails loanDetails = new LoanDetails { LoanDetailsId = 8, LoanId = 17, PaidAmount = 20000, BalanceAmount = 0, BalanceDuration = 22, TotalPaidAmount = 200000 };
            Messages message = new Messages();
            message.Success = false;
            message.Message = " Loan not completed";
            var mockService = new Mock<ILoanDetailRepository>();
            mockService.Setup(x => x.LoanClosed(It.IsAny<int>())).Returns(message);
            var controller = new LoanDetailController(mockService.Object);
            var okResult = controller.LoanClosed(8);
            Assert.IsType<OkObjectResult>(okResult);
        }

    }
}
