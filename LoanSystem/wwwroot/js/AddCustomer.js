$(document).ready(function () {
    $("#AddCustomer").submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Home/Save",
            type: "Post",
            data: $("#AddCustomer").serialize(),
            dataType: 'json',
            success: function (response) {
                alert(response.message);
                if (response.success == true) {
                    setTimeout(function () { window.location = '/Home/CustomerList'; }, 1000);
                }
            },
            error: function () {
                alert("error");
            }
        });
    });
});