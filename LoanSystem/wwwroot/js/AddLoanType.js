$(document).ready(function () {
    $("#AddLoanType").submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Home/SaveLoanType",
            type: "Post",
            data: $("#AddLoanType").serialize(),
            dataType: 'json',
            success: function (response) {
                if (response.message = true) {
                    alert("LoanType added");
                    setTimeout(function () { window.location = '/Home/LoanTypeList'; }, 1000);
                }
                else {
                    alert(response.message);
                    setTimeout(function () { window.location = '/Home/LoanTypeList'; }, 1000);
                }
            },
            error: function () {
                alert("error in adding new loantype");
            }
        });
    });
});