$(document).ready(function () {
    $("#AddLoan").submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Home/SaveLoan",
            type: "Post",
            data: $("#AddLoan").serialize(),
            dataType: 'json',
            success: function (response) {
                alert(response.message);
                if (response.success == true) {
                    setTimeout(function () { window.location = '/Home/LoanList'; }, 1000);
                }
            },
            error: function () {
                alert("error");
            }
        });
    });
});