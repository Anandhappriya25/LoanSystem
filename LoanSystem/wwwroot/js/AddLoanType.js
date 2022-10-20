$(document).ready(function () {
    $('#AddLoanType').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Home/SaveLoanType",
            type: "Post",
            data: $("#AddLoanType").serialize(),
            success: function (response) {
                if (response.success == true) {
                    alert("LoanType added successfully");
                    setTimeout(function () { window.location = '/Home/LoanTypeList'; }, 1000);
                }
                else {
                    alert(response.message);
                    setTimeout(function () { window.location = '/Home/LoanTypeList'; }, 1000);
                }
            }
        });
    });
});