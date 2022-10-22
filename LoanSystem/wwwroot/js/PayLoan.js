$(document).ready(function () {
    $("#PayLoan").submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Home/SaveLoanDetail",
            type: "Post",
            data: $("#PayLoan").serialize(),
            dataType: 'json',
            success: function (response) {
                if (response.success == true) {
                    alert("Loan payed");
                    setTimeout(function () { window.location = '/Home/LoanDetailList'; }, 1000);
                }
                else {
                    alert(response.message);
                    setTimeout(function () { window.location = '/Home/LoanDetailList'; }, 1000);
                }
            },
            error: function () {
                alert("error in adding new loan");
            }
        });
    });
});

//function getDetails() {
//    $.ajax({
//        url: "/Home/GetLoadDetailsById?id=" + $("#LoanId").val(),
//        type: "Get",
//        success: function (response) {
//            console.log(response);
//            if (response != null) {
//                $("#paidAmount").text(response.paidAmount);
//                $("#balanceAmount").text(response.balanceAmount);
//                $("#details").show();
//            }
//            else {
//                alert("Load Id does not exist");
//            }
//        }
//    });
//}

function getDetails() {
    if ($('#LoanId').val()) {
        $.ajax({
            url: "/Home/GetLoadDetailsById?id=" + $("#LoanId").val(),
            type: "Get",
            success: function (response) {
                console.log(response);
                var table = $('#loanList tbody');
                table.html('');
                if (response.length != 0) {

                    $.each(response, function (i, v) {
                        table.append('<tr>').append('<td">' + v.loanId + '</td>')
                            .append('<td>' + v.loanName + '</td>')
                            .append('<td>' + v.customerName + '</td>')
                            .append('<td>' + v.dateOfSanction + '</td>')
                            .append('<td>' + v.loanAmount + '</td>')
                            .append('<td>' + v.paidAmount + '</td>')
                            .append('<td>' + v.balanceAmount + '</td>')
                            .append('<td>' + v.balanceAmount == 0 ? "Loan Completed" : "Loan Pending" + '</td>')
                            .append('</tr>');
                    });
                    $('#loanList').show();
                }
                else {
                    table.append('<tr>').append('<td colspan="7"> No Data to Display </td>')
                        .append('</tr>');
                }
            },
            error: function () {
                alert("error");
            }
        });
    }
    else {
        $('#loanList').hide();
    }
}