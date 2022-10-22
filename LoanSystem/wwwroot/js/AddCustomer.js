$(document).ready(function () {
    $("#AddCustomer").submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Home/Save",
            type: "Post",
            data: $("#AddCustomer").serialize(),
            dataType: 'json',
            success: function (response) {
                if (response.success == true) {
                    alert("Customer added successfully");
                    setTimeout(function () { window.location = '/Home/CustomerList'; }, 1000);
                }
                else {
                    alert(response.message);
                    setTimeout(function () { window.location = '/Home/CustomerList'; }, 1000);
                }
            },
            error: function () {
                alert("error");
            }
        });
    });
});