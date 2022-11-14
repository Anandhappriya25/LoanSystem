$(document).ready(function () {
    $("#PayLoan").submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Home/SaveLoanDetail",
            type: "Post",
            data: $("#PayLoan").serialize(),
            dataType: 'json',
            success: function (response) {
                alert(response.message);
                if (response.success == true){
                    setTimeout(function () { window.location = '/Home/LoanInformation'; }, 1000);                    
                }
                setTimeout(function () { location.reload() });
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
            url: "/Home/GetLoanDetailsId?id=" + $("#LoanId").val(),
            type: "Get",
            success: function (response) {
                console.log(response);
                var table = $('#loanList tbody');
                var html = "";
                if (response.length != 0) {
                    $.each(response, function (i, v) {
                        html += '<tr><td class="text-center">' + v.loanId + '</td>';
                        html += '<td class="text-center">' + v.loanName + '</td>';
                        html += '<td class="text-center">' + v.customerName + '</td>';
                        html += '<td class="text-center">' + v.dateOfSanction + '</td>';
                        html += '<td class="text-center">' + v.loanAmount + '</td>';
                        html += '<td class="text-center">' + v.paidAmount + '</td>';
                        html += '<td class="text-center">' + v.balanceAmount + '</td>';
                        var status = parseInt(v.balanceAmount) == 0 ? "Loan Completed" : "Loan Pending";
                        var cls = parseInt(v.balanceAmount) == 0 ? "btn-primary" : "btn-warning";
                        html += '<td class="text-center"><button class="btn '+ cls +'">' + status + '</button></td>';
                        html += '</tr>';
                    });
                    table.html(html);
                    $('#loanList').show();
                }
                else {
                    alert("No data to display");
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