$(document).ready(function () {
    $('#PayLoan').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Home/SaveLoanDetail",
            type: "Post",
            data: $("#PayLoan").serialize(),
            success: function (response) {
                if (response.success == true) {
                    alert("Loan payed successfully");
                    setTimeout(function () { window.location = '/Home/LoanDetailList'; }, 1000);
                }
                else {
                    alert(response.message);
                    setTimeout(function () { window.location = '/Home/LoanDetailList'; }, 1000);
                }
            }
        });
    });
    //$('#LoanId').onChange(function (e) {
    //});
});

function getDetails() {
    $.ajax({
        url: "/Home/GetLoadDetailsById?id=" + $("#LoanId").val(),
        type: "Get",
        success: function (response) {
            console.log(response);
            if (response != null) {
                $("#paidAmount").text(response.paidAmount);
                $("#details").show();
            }
            else {
                alert("Load Id does not exist");
            }
        }
    });
}