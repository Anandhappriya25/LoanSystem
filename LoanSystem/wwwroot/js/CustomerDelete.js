function ConfirmDelete(id) {
    let result = confirm("Are you sure you want to delete");
    if (result) {
        $.ajax({
            type: "get",
            url: "/Home/DeleteCustomer?id=" + id,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) {
                console.log(response);
                if (response.success == true) {
                    alert("Customer removed");
                    setTimeout(function () { window.location = '/Home/CustomerList'; }, 1000);
                }
                else {
                    alert(response.message);
                }
            },
            error: function () {
                alert("error");
            }
        });
    }
}