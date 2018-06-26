﻿// Global variables
var counter = 1;
var newtransactionreccounter = 1;
var paymentrowscounter = 1;

var res_center = "", sup = "", comp = "", total_amt = "";

/********************/
$(document).on('click', '.addrow', function () {
    var html = '<tr class="a_' + counter + '">' +
        '<td><select id="Account_Id" name="Account_Id[' + counter + ']"></select></td>' +
        '<td><select id="Supplier_Id" name="Supplier_Id[' + counter + ']" class="transparty"></select></td>' +
        '<td><select id="Customer_Id" name="Customer_Id[' + counter + ']" class="transparty"></select></td>' +
        '<td><select id="Employee_Id" name="Employee_Id[' + counter + ']" class="transparty"></select></td>' +
        '<td><select id="Owner_Id" name="Owner_Id[' + counter + ']" class="transparty"></select></td>' +
        '<td><select id="Company_Id" name="Company_Id[' + counter + ']" class=""></select></td>' +
        '<td><select id="transparty" name="transparty[' + counter + ']"></select></td>' +
        '<td> <input type="text" class="" name="Quantity[' + counter + ']" id="Quantity"/></td>' +
        '<td><input type="text" class="" name="Amount_TC[' + counter + ']" id="Amount_TC"/></td>' +
        '<td><input type="text" class="" name="Exchange_Rate[' + counter + ']" id="Exchange_Rate"/></td>' +
        '<td> <input type="text" class="" name="Amount_LC[' + counter + ']" id="Amount_LC"/></td>' +
        '<td><input type="text" class="" name="Description[' + counter + ']" id="Description"/></td>' +
        '<td><input type="text" class="debit" name="Debit[' + counter + ']" id="Debit"/></td>' +
        '<td><input type="text" class="credit" name="Credit[' + counter + ']" id="Credit"/></td>' +
        '<td><input type= "text" name= "Knocking_Off[' + counter + ']"  id="Knocking_Off" class="" /></td >' +
        '<td><select id="Plot" name="Plot[' + counter + ']" class=""></select></td>' +
        '<td><select id="Land" name="Land[' + counter + ']" class=""></select></td>' +
        '<td><select id="ResponsibilityCenter_Id" name="ResponsibilityCenter_Id[' + counter + ']" class=""></select></td>' +
        '<td><select id="Function_Id" name="Function_Id[' + counter + ']" class=""></select></td>' +
        '<td><select id="Project" name="Project[' + counter + ']" class=""></select></td>' +
        '<td><select id="REA" name="REA[' + counter + ']" class=""></select></td>' +
        '<td><select id="REP" name="REP[' + counter + ']" class=""></select></td>' +
        '<td><select id="Space_Type" name="Space_Type[' + counter + ']" class=""></select></td>' +
        '</tr>';

    $('tbody').append(html);
    init();
});
function Trasactionscriptsinit() {
    $('#checkbox1').change(function () {
        if (this.checked) {
            $('.plot, .R_land, .R_resCen, .R_func, .R_prj, .R_rea, .R_rep, .R_sp').show(); // hide the column header th

            $('tr').each(function () {
                $(this).find('td:eq(' + $('.plot').index() + ')').show();
                $(this).find('td:eq(' + $('.R_land').index() + ')').show();
                $(this).find('td:eq(' + $('.R_resCen').index() + ')').show();
                $(this).find('td:eq(' + $('.R_func').index() + ')').show();
                $(this).find('td:eq(' + $('.R_prj').index() + ')').show();
                $(this).find('td:eq(' + $(' .R_rea').index() + ')').show();
                $(this).find('td:eq(' + $('.R_rep').index() + ')').show();
                $(this).find('td:eq(' + $('.R_sp').index() + ')').show();
            });
        }
        else {
            $('.plot, .R_land, .R_resCen, .R_func, .R_prj, .R_rea, .R_rep, .R_sp').hide(); // hide the column header th

            $('tr').each(function () {
                $(this).find('td:eq(' + $('.plot').index() + ')').hide();
                $(this).find('td:eq(' + $('.R_land').index() + ')').hide();
                $(this).find('td:eq(' + $('.R_resCen').index() + ')').hide();
                $(this).find('td:eq(' + $('.R_func').index() + ')').hide();
                $(this).find('td:eq(' + $('.R_prj').index() + ')').hide();
                $(this).find('td:eq(' + $('.R_rea').index() + ')').hide();
                $(this).find('td:eq(' + $('.R_rep').index() + ')').hide();
                $(this).find('td:eq(' + $('.R_sp').index() + ')').hide();
            });
        }


    });
    var screensize = $(window).width();
    var resize = screensize - 320;
    $('.ftcard').css('width', resize + 'px');
}
$(document).on("change", ".transparty", function () {
    var $this = $(this);
    var text = $("option:selected", $this).text();
    var val = $($this).val();
    var trclass = $($this).closest('tr').prop('class');
    $('.' + trclass + ' #transparty').append('<option value=' + val + '>' + text + '</option>');
});
$(document).on('change', '.debit', function () {
    debit = debit + parseFloat($(this).val());
    $('#totaldebit').val(debit);
});
$(document).on('change', '.credit', function () {
    credit = credit + parseFloat($(this).val());
    $('#totalcredit').val(credit);
});
$(document).on('keyup', '.checkdisable', function () {

});



function init() {
    //$('.datepicker').datepicker();
    $('.a_' + counter + ' #transparty').append('<option value="">Select Transaction Party</option>');
    $('.a_' + counter + ' #Account_Id').append('<option value="">Select Account Head</option>');
    $.each(acc, function (key, value) {
        $('.a_' + counter + ' #Account_Id').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #Supplier_Id').append('<option value="">Select Supplier</option>');
    $.each(sup, function (key, value) {
        $('.a_' + counter + ' #Supplier_Id').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #Customer_Id').append('<option value="">Select Customer</option>');
    $.each(cust, function (key, value) {
        $('.a_' + counter + ' #Customer_Id').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #Employee_Id').append('<option value="">Select Employee</option>');
    $.each(emp, function (key, value) {
        $('.a_' + counter + ' #Employee_Id').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #Owner_Id').append('<option value="">Select Owner</option>');
    $.each(owner, function (key, value) {
        $('.a_' + counter + ' #Owner_Id').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #Company_Id').append('<option value="">Select Company</option>');
    $.each(comp, function (key, value) {
        $('.a_' + counter + ' #Company_Id').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #plot').append('<option value="">Select Plot</option>');
    $.each(plot, function (key, value) {
        $('.a_' + counter + ' #plot').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #land').append('<option value="">Select Land</option>');
    $.each(land, function (key, value) {
        $('.a_' + counter + ' #land').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #ResponsibilityCenter_Id').append('<option value="">Select Responsibility Center</option>');
    $.each(respo, function (key, value) {
        $('.a_' + counter + ' #ResponsibilityCenter_Id').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #Function_Id').append('<option value="">Select Function</option>');
    $.each(func, function (key, value) {
        $('.a_' + counter + ' #Function_Id').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #Project').append('<option value="">Select Project</option>');
    $.each(proj, function (key, value) {
        $('.a_' + counter + ' #Project').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #REA').append('<option value="">Select REA</option>');
    $.each(rea, function (key, value) {
        $('.a_' + counter + ' #REA').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #REP').append('<option value="">Select REP</option>');
    $.each(rep, function (key, value) {
        $('.a_' + counter + ' #REP').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    $('.a_' + counter + ' #Space_Type').append('<option value="">Select Space Type</option>');
    $.each(space, function (key, value) {
        $('.a_' + counter + ' #Space_Type').append('<option value=' + value.Value + '>' + value.Text + '</option>');
    });
    counter++;

}
$(document).ready(function () {
    var screensize = $(window).width();
    var resize = screensize - 320;
    $('.ftcard').css('width', resize + 'px');
});
$(document).on("click", ".add-transaction", function () {
    newtransactionreccounter++;
    var html = '<div id="trans-' + newtransactionreccounter + '">' +
        '<div class="row">' +
        '<div class="fa-hover col-md-10 col-sm-10 col-xs-12">' +
        '<a href="javascript:void" class="rm-transaction" data-rowindex="' + newtransactionreccounter + '"><i class="fa fa-minus-square"></i></a>' +
        '</div>' +
        '<button class="btn btn-primary searchinv" data-searchrow="' + newtransactionreccounter + '" data-toggle="modal" data-target=".bs-example-modal-lg" type="button">Search</button>' +
        '</div>' +
        '<div class="row">' +
        '<div class="col-sm-3"><label>Voucher Date</label><div class="form-group"><input id="Voucher_Date" name="Date" required class="form-control date-picker" readonly></div></div>' +
        '<div class="col-sm-3"><label>Supplier</label><div class="form-group"><select id="supplier" class="form-control"></select></div></div>' +
        '<div class="col-sm-3"><label>Responsibility Center</label><div class="form-group"><select id="responsibility-center" class="form-control"></select></div></div>' +
        '<div class="col-sm-3"><label>Transaction Type</label><div class="form-group"><select id="trans-type" class="form-control"><option value="IR">Invoice Recording</option><option value="CN">Credit Note</option><option value="DN">Debit Note</option><option value="IN">Immediate Note</option><option value="PP">Pre Payment</option><option value="AA">Adjust Advance</option></select></div></div>' +
        '<div class="col-sm-3"><label>Nature</label><div class="form-group"><select id="nature" class="form-control"></select></div></div>' +
        '<div class="col-sm-3"><label>Company</label><div class="form-group"><select id="company" class="form-control"></select></div></div>' +
        '</div>' +
        '<div class="row">' +
        '<div class="col-sm-3"><label>Total Federal Excise Duty</label><div class="form-group"><input id="fedral-tax" class="form-control" readonly></div></div>' +
        '<div class="col-sm-3"><label>Total Advance Tax</label><div class="form-group"><input id="wth-tax" class="form-control" readonly></div></div>' +
        '<div class="col-sm-3"><label>Total GST</label><div class="form-group"><input type="text" id="sales-tax" class="form-control" readonly></div></div>' +
        '<div class="col-sm-3"><label>Total Amount</label><div class="form-group"><input type="text" id="total-amt" class="form-control totalamt" readonly></div></div>' +
        '</div>' +
        '<div class="x_panel">' +
        '<div class="x_content table-responsive"><table class="table table-bordered" id="transrectable"><thead>' +
        '<tr>' +
        '<th style="min-width:170px">Invoice</th>' +
        '<th style="min-width:100px">Purchase Order </th>' +
        '<th style="min-width:100px">Product</th>' +
        '<th style="min-width:350px">Description </th>' +
        '<th style="min-width:100px">Quantity</th>' +
        '<th style="min-width:100px">UOM</th>' +
        '<th style="min-width:100px">Rate</th>' +
        '<th style="min-width:100px">Amount(TC)</th>' +
        '<th style="min-width:100px">Exchange Rate</th>' +
        '<th style="min-width:100px">Currency</th>' +
        '<th style="min-width:100px">Amount(LC) Excl. Tax</th>' +
        '<th style="min-width:100px">Fedral Excise Duty</th>' +
        '<th style="min-width:100px">GST</th>' +
        '<th style="min-width:100px">Advance Tax</th>' +
        '<th style="min-width:100px">Amount(LC) Incl. Tax</th>' +
        '<th style="min-width:100px">Customer </th>' +
        '<th style="min-width:100px">Employee </th>' +
        '<th style="min-width:100px">Shareholder </th>' +
        '<th style="min-width:100px">Beneficiary Department </th>' +
        '<th style="min-width:100px">Region </th>' +
        '<th style="min-width:100px">Business Line </th>' +
        '<th style="min-width:100px">Project</th>' +
        '</tr>' +
        '</thead>' +
        '<tbody>' +
        '<tr class="1">' +
        '<td><input class="form-control invoice"></td>' +
        '<td><input class="form-control ponum"></td>' +
        '<td><input type="text" class="form-control prod"></td>' +
        '<td><input class="form-control desc"></td>' +
        '<td><input class="form-control qty" type="number"></td>' +
        '<td><input class="form-control uom"></td>' +
        '<td><input class="form-control rate"></td>' +
        '<td><input class="form-control amt-tc amt-tc-ex-rate"></td>' +
        '<td><input class="form-control exch-rate amt-tc-ex-rate"></td>' +
        '<td><select class="form-control curr"><option value="PKR" selected="selected">PKR</option><option value="USD">USD</option></select></td>' +
        '<td><input class="form-control amt-lc-ex sumall" formate="number"></td>' +
        '<td><input class="form-control fedtax sumall" formate="number"></td>' +
        '<td><input class="form-control gst sumall" formate="number"></td>' +
        '<td><input class="form-control wth sumall" formate="number"></td>' +
        '<td><input class="form-control amt-lc-in"></td>' +
        '<td><input class="form-control cust"></td>' +
        '<td><input class="form-control emp"></td>' +
        '<td><input class="form-control sharehold"></td>' +
        '<td><input class="form-control benf-dep"></td>' +
        '<td><input class="form-control region"></td>' +
        '<td><input class="form-control busines-line"></td>' +
        '<td><input class="form-control proj"></td>' +
        '</tr>' +
        '</tbody>' +
        '</table>' +
        '</div>' +
        '</div>' +
        '</div>';

    $('.all-forms').append(html);
});
$(document).on("click", ".rm-transaction", function () {
    newtransactionreccounter--;
    var id = $(this).data("rowindex");
    var voucher_num = $("#trans-" + id + " #voucher_num").val();
    $('#paymentcontroller tbody #t').remove();
    $("#trans-" + id).remove();
});

$(document).on("click", ".get-transaction", function () {
    var vn = $(this).attr("id");
    $.ajax({
        traditional: true,
        type: "POST",
        data: { Voucher_Number: vn },
        url: '/PurchaseAndPayables/GetTransaction/',
        success: function (data) {
            $(".modal").modal("hide");
            $("#transaction-form .payinv").prop("disabled", false);
            $("#transaction-form .posttrans").prop("disabled", true);
            $("#transaction-form #trans-" + rowid + " #transrectable tbody").empty();
            var groupedData = _.groupBy(data.Trans, function (d) { return d.Product; });
            var html = "";

            _.each(
                _.toArray(groupedData), function (num) {
                    var GSTROW = num.filter(x => x.Account_Id == 1052); // get the General Sales Tax row
                    var WTHROW = num.filter(x => x.Account_Id == 1053); // get the With Holding Tax row
                    var FedROW = num.filter(x => x.Account_Id == 1051); // get the federal tax row

                    var gstvalue = $.map(GSTROW, function (a, b) { return a.Amount_LC });
                    var wthvalue = $.map(WTHROW, function (a, b) { return a.Amount_LC });
                    var fedvalue = $.map(FedROW, function (a, b) { return a.Amount_LC });

                    gstvalue = parseFloat(gstvalue[0] || 0);
                    wthvalue = parseFloat(wthvalue[0] || 0);
                    fedvalue = parseFloat(fedvalue[0] || 0);
                    var inventory = num.filter(x => x.Account_Id == 1057); // get the inventory
                    var supaccpay = num.filter(x => x.Account_Id == 1059); // get the supplier .. account payables
                    var inctaxtotal = gstvalue + wthvalue + fedvalue + parseFloat(inventory[0].Amount_LC);
                    {
                        html = '<tr class="rows-for-payment">' +
                            '<td><input class="form-control invoice" value="' + inventory[0].Transaction_Document_Number + '"></td>' +
                            '<td><input class="form-control ponum" value="' + inventory[0].Purchase_Order + '"></td>' +
                            '<td><input type="text" class="form-control prod" value="' + inventory[0].Product + '"></td>' +
                            '<td><input class="form-control desc" value="' + inventory[0].Description + '"></td>' +
                            '<td><input class="form-control qty" type="number" value="' + inventory[0].Quantity + '"></td>' +
                            '<td><input class="form-control uom" value="' + inventory[0].Uom + '"></td>' +
                            '<td><input class="form-control rate" value="' + inventory[0].Rate + '"></td>' +
                            '<td><input class="form-control amt-tc amt-tc-ex-rate" value="' + inventory[0].Amount_TC + '"></td>' +
                            '<td><input class="form-control exch-rate amt-tc-ex-rate" value="' + inventory[0].Exchange_Rate + '"></td>' +
                            '<td><select class="form-control curr"><option value="PKR" selected="selected">PKR</option><option value="USD">USD</option></select></td>' +
                            '<td><input class="form-control amt-lc-ex sumall" formate="number" value="' + inventory[0].Amount_LC + '"></td>' +
                            '<td><input class="form-control fedtax sumall" formate="number" value="' + fedvalue + '"></td>' +
                            '<td><input class="form-control gst sumall" formate="number" value="' + gstvalue + '"></td>' +
                            '<td><input class="form-control wth sumall" formate="number" value="' + wthvalue + '"></td>' +
                            '<td><input class="form-control amt-lc-in" value="' + inctaxtotal + '"></td>' +
                            '<td><input class="form-control cust" value="' + inventory[0].Customer_Id + '"></td>' +
                            '<td><input class="form-control emp" value="' + inventory[0].Employee_Id + '"></td>' +
                            '<td><input class="form-control sharehold" value="' + inventory[0].Owner_Id + '"></td>' +
                            '<td><input class="form-control benf-dep" value="' + inventory[0].Function_Id + '"></td>' +
                            '<td><input class="form-control region" value="' + inventory[0].Region + '"></td>' +
                            '<td><input class="form-control busines-line" value="' + inventory[0].REP + '"></td>' +
                            '<td><input class="form-control proj" value="' + inventory[0].Project + '"></td>' +
                            '</tr>';
                    }
                    {
                        var paymentrows = '<tr class="' + inventory[0].Voucher_Number + ' ' + paymentrowscounter + '">' +
                            '<td><input class="form-control invoice" value="' + inventory[0].Transaction_Document_Number + '"></td>' +
                            '<td><input class="form-control ponum" value="' + inventory[0].Purchase_Order + '"></td>' +
                            '<td><input type="text" class="form-control prod" value="' + inventory[0].Product + '"></td>' +
                            '<td><input class="form-control desc" value="' + inventory[0].Description + '"></td>' +
                            '<td><input class="form-control qty" type="number" value="' + inventory[0].Quantity + '"></td>' +
                            '<td><input class="form-control uom" value="' + inventory[0].Uom + '"></td>' +
                            '<td><input class="form-control rate" value="' + inventory[0].Rate + '"></td>' +
                            '<td><input class="form-control gross-amt" value="' + inctaxtotal + '"></td>' +
                            '<td><input class="form-control disc"></td>' +
                            '<td><input class="form-control amt-tc" value="' + inctaxtotal + '"></td>' +
                            '<td><input class="form-control exch-rate" value="' + supaccpay[0].Exchange_Rate + '"></td>' +
                            '<td><select class="form-control curr"><option value="PKR" selected="selected">PKR</option><option value="USD">USD</option></select></td>' +
                            '<td><input class="form-control amt-lc-ex" formate="number" value="' + inctaxtotal + '"></td>' +
                            '<td><input class="form-control wth2 sumrowpayment" formate="number" value="0"></td>' +
                            '<td><input class="form-control amt-lc-in"  value="' + inctaxtotal + '"></td>' +
                            '<td><input class="form-control cust" value="' + inventory[0].Customer_Id + '"></td>' +
                            '<td><input class="form-control emp" value="' + inventory[0].Employee_Id + '"></td>' +
                            '<td><input class="form-control sharehold" value="' + inventory[0].Owner_Id + '"></td>' +
                            '<td><input class="form-control benf-dep" value="' + inventory[0].Function_Id + '"></td>' +
                            '<td><input class="form-control region" value="' + inventory[0].Region + '"></td>' +
                            '<td><input class="form-control busines-line" value="' + inventory[0].REP + '"></td>' +
                            '<td><input class="form-control proj"  value="' + inventory[0].Project + '"></td>' +
                            '<td style="display:none"><input class="form-control vou-num"  value="' + inventory[0].Voucher_Number + '"></td>' +
                            '</tr>';

                        $('#paymentcontroller tbody').append(paymentrows);
                        paymentrowscounter++;
                    }
                    $("#transaction-form #trans-" + rowid + " #transrectable tbody").append(html);
                }

            );
            $.each(data.Sumary, function (i) {
                if (data.Sumary[i].COA == 1052) { // get the General Sales Tax row
                    $('#transaction-form  #trans-' + rowid + ' #sales-tax').val(data.Sumary[i].Debit || 0);
                }
                else if (data.Sumary[i].COA == 1053) {  // get the With Holding Tax row
                    $('#transaction-form #trans-' + rowid + ' #wth-tax').val(data.Sumary[i].Debit || 0);
                }
                else if (data.Sumary[i].COA == 1051) {  // get the federal tax row
                    $('#transaction-form #trans-' + rowid + ' #fedral-tax').val(data.Sumary[i].Debit || 0);
                }
                else if (data.Sumary[i].COA == 1059) {   // get other info
                    total_amt = data.Sumary[i].Credit || 0; sup = data.Sumary[i].Supplier_Id;
                    comp = data.Sumary[i].Company_Id; res_center = data.Sumary[i].ResponsibilityCenter_Id;
                    $('#transaction-form #trans-' + rowid + ' #total-amt').val(total_amt);
                    $('#transaction-form #trans-' + rowid + ' #Voucher_Date').val(moment(data.Sumary[i].Document_Date).format("DD-MM-YYYY"));
                    $('#transaction-form #trans-' + rowid + ' #trans-type').val(data.Sumary[i].Voucher_Type);
                    $('#transaction-form #trans-' + rowid + ' #company').val(comp);
                    $('#transaction-form #trans-' + rowid + ' #supplier').val(sup);
                    $('#transaction-form #trans-' + rowid + ' #responsibility-center').val(res_center);
                    $('#transaction-form #trans-' + rowid + ' #voucher_num').val(data.Sumary[i].Voucher_Number);
                }
            });
        },
        error: function () {
            alert("Error")
        }
    })
});
$(document).on("change", "#payment-form #payment_type", function () {
    var cash_bank = $(this).val();
    if (cash_bank == "bank") {
        $("#payment-form #cash-bank").show();
    }
    else {
        $("#payment-form #cash-bank").hide();
    }
});
function updatetotalamt() {
    var TotalAmt = 0;
    $('.amt-lc-in').each(function () {
        TotalAmt = TotalAmt + parseFloat($(this).val() || 0);
    });
    $('#total-amt').val(TotalAmt);
}
$(document).on("click", ".payinv", function () {
    $(".paymenttable tbody").empty();
    $(".paymenttable tbody").append($('#paymentcontroller tbody').html());
    $('#payment-form #company').val(comp);
    $('#payment-form #supplier').val(sup);
    $('#payment-form #responsibility-center').val(res_center);
    var sum = 0;
    $('.totalamt').each(function () {
        sum += parseFloat($(this).val());
    });
    $('#payment-form #total-amt').val(sum);
});
$(document).on("click", ".searchinv", function () {
    var transrowid = $(this).data("searchrow");
    $(".modal-body").load('/PurchaseAndPayables/SearchInvoiceBody/', { Rowid: transrowid });
});
function CheckTransType(Type, who, Amount, data) {
    switch (Type) {
        case "IR":
            if (who == "supplier") {
                data.Credit = Amount;
            }
            else if (who == "other") {
                data.Debit = Amount;
            }
            break;
        case "CN":
            if (who == "supplier") {
                data.Credit = Amount;
            }
            else if (who == "other") {
                data.Debit = Amount;
            }
            break;
        case "DN":
            if (who == "supplier") {
                data.Debit = Amount;
            }
            else if (who == "other") {
                data.Credit = Amount;
            }
            break;
        case "IN":
            break;
        case "PP":
            break;
        case "AA":
            break;
    }
}
