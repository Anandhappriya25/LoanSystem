$(document).ready(function () {
    $("#AddLoan").submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Home/SaveLoan",
            type: "Post",
            data: $("#AddLoan").serialize(),
            dataType: 'json',
            success: function (response) {
                if (response.message = true) {
                    alert("Loan added");
                    setTimeout(function () { window.location = '/Home/LoanList'; }, 1000);
                }
                else {
                    alert(response.message);
                    setTimeout(function () { window.location = '/Home/LoanList'; }, 1000);
                }
            },
            error: function () {
                alert("error in adding new loan");
            }
        });
    });
});