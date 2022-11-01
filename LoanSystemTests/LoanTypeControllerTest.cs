using LoanSystem.Controllers;
using LoanSystem.Models;
using LoanSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestCase
{
    public class LoanTypeControllerTest
    {
        [Fact]
        public void GetAllLoanType_ReturnOk()
        {
            LoanType loanType = new LoanType { LoanTypeId = 5, LoanName = "Property Loan", Duration = 24 };
            LoanType loanType2 = new LoanType { LoanTypeId = 4, LoanName = "Car Loan", Duration = 24 };
            List<LoanType> loanTypes = new List<LoanType>();
            loanTypes.Add(loanType);
            loanTypes.Add(loanType2);
            var mockservice = new Mock<ILoanTypeRepository>();
            mockservice.Setup(x => x.GetAll()).Returns(loanTypes);
            var controller = new LoanTypeController(mockservice.Object);
            var okresult = controller.GetAll();
            Assert.IsType<OkObjectResult>(okresult);
        }

        [Fact]
        public void GetAllLoanType_ReturnNotFound()
        {
            List<LoanType> loanTypes = new List<LoanType>();
            var mockservice = new Mock<ILoanTypeRepository>();
            mockservice.Setup(x => x.GetAll()).Returns(loanTypes);
            var controller = new LoanTypeController(mockservice.Object);
            var result = controller.GetAll();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetByLoanTypeId_ReturnOK()
        {
            LoanType loanType = new LoanType { LoanTypeId = 5, LoanName = "Property Loan", Duration = 24 };
            List<LoanType> loanTypes = new List<LoanType>();
            loanTypes.Add(loanType);
            var mockservice = new Mock<ILoanTypeRepository>();
            mockservice.Setup(x => x.GetById(It.IsAny<int>())).Returns(loanType);
            var controller = new LoanTypeController(mockservice.Object);
            var okresult = controller.GetByLoanTypeId(3) as OkObjectResult;
            Assert.NotNull(okresult);
            var model = okresult.Value as LoanType;
            Assert.StrictEqual(loanType.LoanTypeId, model.LoanTypeId);
        }

        [Fact]
        public void GetByLoanTypeId_ReturnNotFound()
        {
            LoanType loanType = null;
            var mockservice = new Mock<ILoanTypeRepository>();
            mockservice.Setup(x => x.GetById(It.IsAny<int>())).Returns(loanType);
            var controller = new LoanTypeController(mockservice.Object);
            var result = controller.GetByLoanTypeId(30);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddLoanType_ReturnOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "Added";
            LoanType loanType = new LoanType { LoanTypeId = 9, LoanName = "Bike Loan", Duration = 24 };
            var mockservice = new Mock<ILoanTypeRepository>();
            mockservice.Setup(x => x.AddLoanType(It.IsAny<LoanType>())).Returns(messages);
            var controller = new LoanTypeController(mockservice.Object);
            var result = controller.AddLoanType(loanType);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void AddLoanType_ReturnExists()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "Not Added";
            LoanType loantype = new();
            var mockservice = new Mock<ILoanTypeRepository>();
            mockservice.Setup(x => x.AddLoanType(It.IsAny<LoanType>())).Returns(messages);
            var controller = new LoanTypeController(mockservice.Object);
            var result = controller.AddLoanType(loantype);
            //Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateLoanType_Return()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "updated";
            LoanType loanType = new LoanType { LoanTypeId = 5, LoanName = "Jewel Loan", Duration = 12 };
            var mockservice = new Mock<ILoanTypeRepository>();
            mockservice.Setup(x => x.UpdateLoanType(It.IsAny<LoanType>())).Returns(messages);
            var controller = new LoanTypeController(mockservice.Object);
            var result = controller.UpdateLoanType(loanType) as ObjectResult;
            Assert.IsType<OkObjectResult>(result);
            //var model = result.Value as LoanType;
            //Assert.Equal(model.LoanTypeId, loanType.LoanTypeId);
        }

        [Fact]
        public void UpdateLoanType_ReturnException()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "not updated";
            LoanType loanType = new LoanType();
            var mockservice = new Mock<ILoanTypeRepository>();
            mockservice.Setup(x => x.UpdateLoanType(It.IsAny<LoanType>())).Returns(messages);
            var controller = new LoanTypeController(mockservice.Object);

            var result = Assert.Throws<Exception>(() => controller.UpdateLoanType(loanType));
            Assert.IsType<Exception>(result);
            Assert.Equal("The LoantypeId not exists", result.Message);
        }

        [Fact]
        public void DeleteLoanType_ReturnOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "deleted";
            LoanType loanType = new LoanType { LoanTypeId = 5, LoanName = "Property Loan", Duration = 24 };
            var mockservice = new Mock<ILoanTypeRepository>();
            mockservice.Setup(x => x.DeleteLoanType(It.IsAny<int>())).Returns(messages);
            var controller = new LoanTypeController(mockservice.Object);
            var delete = controller.DeleteLoanType(5) as OkObjectResult;
            Assert.IsType<OkObjectResult>(delete);
            //var result = delete.Value as LoanType;
            //Assert.Equal(result.LoanName, loanType.LoanName);
        }
    }
}
