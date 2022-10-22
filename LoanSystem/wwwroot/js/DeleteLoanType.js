function Confirm(id) {
    let result = confirm("Are you sure you want to delete");
    if (result) {
        $.ajax({
            type: "get",
            url: "/Home/DeleteLoanType?id=" + id,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) {
                if (response.success == true) {
                    alert("LoanType Deleted");
                    setTimeout(function () { window.location = '/Home/LoanTypeList'; }, 1000);
                }
                else {
                    alert(response.message);
                    setTimeout(function () { window.location = '/Home/LoanTypeList'; }, 1000);
                }
            },
            error: function () {
                alert("error in deleting loantype");
            }
        });
    }
}