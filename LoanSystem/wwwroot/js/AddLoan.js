$(document).ready(function () {
    $('#AddLoan').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Home/SaveLoan",
            type: "Post",
            data: $("#AddLoan").serialize(),
            success: function (response) {
                if (response.success == true) {
                    alert("Loan added successfully");
                    setTimeout(function () { window.location = '/Home/LoanList'; }, 1000);
                }
                else {
                    alert(response.message);
                    setTimeout(function () { window.location = '/Home/LoanList'; }, 1000);
                }
            }
        });
    });
});