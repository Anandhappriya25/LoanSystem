$(document).ready(function () {
    $("#AddLoanType").submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Home/SaveLoanType",
            type: "Post",
            data: $("#AddLoanType").serialize(),
            dataType: 'json',
            success: function (response) {
                alert(response.message);
                if (response.success == true) {
                    setTimeout(function () { window.location = '/Home/LoanTypeList'; }, 1000);
                }
            },
            error: function () {
                alert("error in adding new loantype");
            }
        });
    });
});