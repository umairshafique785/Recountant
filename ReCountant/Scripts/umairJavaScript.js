




// here transaction recording setup tab user added as supplier***************************88



$('.Html_TextBox_Hidden_members_Setup_cnic').hide();
$('.Html_TextBox_Hidden_members_Setup_ind_aop_comp').hide();
$('.hide_add_as_supplier_link').hide();

$('.Cus_IAP_check').change(function () {

    var individual_aop_company_check = $('.Cus_IAP_check :selected').text();
    if (individual_aop_company_check == "Individual")
    {
        $('.Html_TextBox_Hidden_members_Setup_cnic').show();
        $('.Html_TextBox_Hidden_members_Setup_ind_aop_comp').hide();
        $('.hide_add_as_supplier_link').show();
        $('.aop_company_name  ').val("");
        $('.aop_company_name_id').val("");


    }
    else if (individual_aop_company_check == "AOP") {
        $('.Html_TextBox_Hidden_members_Setup_cnic').hide();
        $('.Html_TextBox_Hidden_members_Setup_ind_aop_comp').show();
        $('.uCNIC_Number').val("");
        $('.uGender').val("");
        $('.usercnic').val("");
        $('.hide_add_as_supplier_link').show();
    }
    else if (individual_aop_company_check == "Company")
    {
        $('.Html_TextBox_Hidden_members_Setup_cnic').hide();
        $('.Html_TextBox_Hidden_members_Setup_ind_aop_comp').show();
        $('.uCNIC_Number').val("");
        $('.uGender').val("");
        $('.usercnic ').val("");
        $('.hide_add_as_supplier_link').show();
    }
    else
    {
        $('.Html_TextBox_Hidden_members_Setup_cnic').hide();
        $('.Html_TextBox_Hidden_members_Setup_ind_aop_comp').hide();
        $('.uCNIC_Number').val("");
        $('.uGender').val("");
        $('.usercnic').val("");
        $('.hide_add_as_supplier_link').hide();

    }

});

function searchuserinfo() {
    var cnic = $('.usercnic').val();
    var indivi_Aop_company = $('.aop_company_name_id').val();
    var drop_ind_aop_com = $('.Cus_IAP_check :selected').text();
        $.ajax({
            type: "POST",
            data: { cnic_user: cnic, user_name_id: indivi_Aop_company, selectes_category: drop_ind_aop_com},
            url: "/Home/GetUserInfo/",
            success: function (data) {
                if (data == false) {
                    UserNotFound();
                }
                else {
                    $('.Cus_Id').val(data.Id);
                    $('.Cus_name').val(data.Name);
                    $('.Cus_address').val(data.Address);
                    $('.Cus_cnic').val(data.CNIC_Number);
                    $('.Cus_gender').val(data.Gender).attr('hidden', true);
                    $('.Cus_ntn').val(data.NTN);
                    $('.Cus_strn').val(data.STRN);
                    $('.Cus_filer').val(data.Filer_NonFiler).attr('hidden', true);
                    $('.Cus_ind').val(data.Industry);
                    $('.Cus_res').val(data.Residential_Status).attr('hidden', true);
                    $('.Cus_buss').val(data.Business);
                    $('.Cus_SCR').val(data.Supply_Chain_Role);
                    $('.Cus_IAP').val(data.Individual_AOP_Company).attr('hidden', true);
                    $('.Cus_email').val(data.Email);
                    $('.Cus_epz').val(data.EPZ);
                    $('.Cus_num').val(data.PhoneNumber);


                }
            },
            error: function () {

            }
        })
}

//***************************************************


function userRole() {

    var role = $('#Cus_role').val();
    var cnic = $('.usercnic').val();

    $.ajax({
        type: "POST",
        data: {
            id: role,
            c: cnic
        },
        url: "/Users/PostUserRole/",
        success: function (response) {

            if (response.success) {
                alert(response.Message);   
                
            }
            else {
                
                swal('Already Exist!')
            }
            }
       
    });

    function succ() {

        alert("User Role Successflly Added");

        ClearUserRoleTextboxes();

    }
    function err() {
        alert("An error has occured!!!");
    }

    function ClearUserRoleTextboxes() {
            $('#Cus_Id').val(""),
            $('.usercnic').val(""),
            $('#Cus_cnic').val(""),
            $('#Cus_name').val(""),
            $('#Cus_address').val(""),
            $('#Cus_num').val(""),
            $('#Cus_gender').val(""),
            $('#Cus_ntn').val(""),
            $('#Cus_strn').val(""),
            $('#Cus_filer').val(""),
            $('#Cus_ind').val(""),
            $('#Cus_res').val(""),
            $('#Cus_buss').val(""),
            $('#Cus_email').val(""),
            $('#Cus_epz').val(""),
            $('#Cus_SCR').val(""),
            $('Cus_IAP').val(""),
            $('Cus_role').val("")
    }
}
var newUserRole = 1;

$(document).on("click", ".user-search", function () {
    newUserRole++;
    var html = '<div id="user-' + newUserRole + '" class="hide_add_as_supplier">' +


        '<div class="row">' +
        '<div class="fa-hover col-md-10 col-sm-10 col-xs-12">' +
        '<a href="javascript:void" class="rm-userRole" data-rowindex="' + newUserRole + '"><i class="fa fa-minus-square"></i></a>' +
        
        '</div>' +
       
        '<div class="x_content">' +
        '<div class="form-horizontal form-label-left">'+
        '<div class="row">' +
        '<input id="Cus_Id"  type="hidden">' +
        ' <input  id="" type="hidden" class="usercnic" />' +

        '<input name="Name" id="" class="form-control Cus_nam" type="hidden" name="Name">' +
        '<input name"CNIC_Number" id="" class="form-control Cus_cni" type="hidden" name="CNIC_Number" >' +
        '<input  id="" class="form-control Cus_addres"  type="hidden" >' +
        //'<select name="Cus_gende" id="Cus_gende" class="form-control" hidden><option value=""></option><option value="male">Male</option><option value="female">Female</option></select>' +
        '<input name="Cus_nt" id=""  class="form-control Cus_nt" type="hidden" >' +
        '<input name="Cus_str" id="" class="form-control Cus_str" type="hidden" >' +
        //'<select name="Cus_re" id="Cus_re" class="form-control" hidden><option value=""> </option><option value="yes">Yes</option><option value="no">No</option></select>' +
        '<input name="" id="Cus_in" class="form-control Industry" type="hidden" >' +
        //'<select  id="Cus_file" class="form-control" hidden><option value=""></option><option value="filer">Yes</option><option value="nonfiler">No</option></select>' +
        //'<select  id="Cus_IA" class="form-control" ><option value=""></option><option value="individual">Individual</option><option value="aop">AOP</option><option value="company">Company</option></select>' +
        '<input name="Business" id="" class="form-control Cus_bus" type="hidden" >' +
        '<input name="Supply_Chain_Role" id="" class="form-control Cus_SC"  type="hidden" >' +
        '<input  id="" class="form-control Cus_ep" type="hidden" >' +
        '<input type="hidden" class="form-control Cus_emai"  id="" >' +
        '<input type="hidden" class="form-control Cus_nu"  id="" >' +
        
        '</div>' +
        '<div class="row">' +
        
        //'<div class="col-sm-3"><label >Name</label><div class="form-group"><input name="Name" id="Cus_nam" class="form-control" type="hidden" name="Name" readonly></div></div>' +
        //'<div class="col-sm-3"><label >CNIC</label ><div class="form-group"><input name"CNIC_Number" id="Cus_cni" class="form-control" type="hidden" name="CNIC_Number" readonly></div></div>' +
        //'<div class="col-sm-3"><label >Address</label><div class="form-group"><input  id="Cus_addres" class="form-control"  type="hidden" readonly></div></div>' +
        //'<div class="col-sm-3"><label >Gender</label><div class="form-group"><select name="Cus_gende" id="Cus_gende" class="form-control" disabled><option value=""></option><option value="male">Male</option><option value="female">Female</option></select></div></div>' +

        //'<div class="col-sm-3"><label >NTN</label><div class="form-group"><input name="Cus_nt" id="Cus_nt"  class="form-control" type="hidden" readonly></div></div>' +
        //'<div class="col-sm-3"><label >STRN</label><div class="form-group"><input name="Cus_str" id="Cus_str" class="form-control" type="hidden" readonly></div></div>' +
        //'<div class="col-sm-3"><label >Residentail Status</label><div class="form-group"><select name="Cus_re" id="Cus_re" class="form-control" disabled><option value=""> </option><option value="yes">Yes</option><option value="no">No</option></select></div></div >' +
        //'<div class="col-sm-3"><label >Industry</label><div class="form-group"><input name="Industry" id="Cus_in" class="form-control" type="hidden" readonly></div></div>' +
        //'<div class="col-sm-3"><label >Filer</label ><div class="form-group"><select  id="Cus_file" class="form-control" disabled><option value=""></option><option value="filer">Yes</option><option value="nonfiler">No</option></select></div></div >' +
        //'<div class="col-sm-3"><label >Legal Status</label ><div class="form-group"><select  id="Cus_IA" class="form-control" ><option value=""></option><option value="individual">Individual</option><option value="aop">AOP</option><option value="company">Company</option></select></div></div >' +
        //'<div class="col-sm-3"><label >Business</label ><div class="form-group"><input name="Business" id="Cus_bus" class="form-control" type="text" readonly></div></div>' +
        //'<div class="col-sm-3"><label >Supply Chain Role</label ><div class="form-group"><input name="Supply_Chain_Role" id="Cus_SC" class="form-control"  type="text" readonly></div></div>' +
        //'<div class="col-sm-3"><label >EPZ</label ><div class="form-group"><input  id="Cus_ep" class="form-control" type="text" readonly></div></div>' +
        //'<div class="col-sm-3"><label >Email</label ><div class="form-group"><input type="hidden" class="form-control"  id="Cus_emai" readonly></div></div>' +
        //'<div class="col-sm-3"><label >Phone Number</label ><div class="form-group"><input type="hidden" class="form-control"  id="Cus_nu" readonly></div></div>' +

        '<div class="col-sm-3"><label >Billing Adress</label ><div class="form-group"><input type="text" class="form-control Cus_Ba" name="Billing_Address" id=""></div></div>' +
        '<div class="col-sm-3"><label >Shipping Adress</label ><div class="form-group"><input type="text" class="form-control Cus_Sa" id="" name="Shipping_Address"></div></div>' +
        '<div class="col-sm-3"><label >Billing Contact Number</label ><div class="form-group"><input type="text" class="form-control Cus_Bcn" name="Billing_Contact_Number" id=""></div></div>' +
        '<div class="col-sm-3"><label >Shipping Contact Number</label ><div class="form-group"><input type="text" class="form-control Cus_Scn" id="" name="Shipping_Contact_Number"></div></div>' +
        '<div class="col-sm-3"><label >Billing Email </label ><div class="form-group"><input type="text" class="form-control Cus_Bei" id="" name="Billing_Email_Id"></div></div>' +
        '<div class="col-sm-3"><label >Shipping Email </label ><div class="form-group"><input type="text" class="form-control Cus_Sei" id="" name="Shipping_Email_Id"></div></div>' +
        '<div class="col-sm-3"><label >Product Type</label ><div class="form-group"><input type="text" class="form-control Cus_Pt" id="" name="Product_Type"></div></div>' +
        '<div class="col-sm-3"><label >Product Method</label ><div class="form-group"> <div class="form-group"><select class="form-control Cus_PM" ><option value="invoice_based">Invoice</option><option value="running_balance_based">Running Balance</option></select ></div></div>' +
        '</div>'+
        '</div>'+
        '</div>'+
        '</div>'+
        '<div class="row">' +
        '<div><button  class="button-grid-blue AddSupp" > Submit</button ></div >' +
        '</div>';
        

    $('.all-user-forms').append(html);
});
$(document).on("click", ".rm-userRole", function () {
    newUserRole--;
    var id = $(this).data("rowindex");
    //var voucher_num = $("#trans-" + id + " #voucher_num").val();
    //$('#paymentcontroller tbody #t').remove();
    $("#user-" + id).remove();
});

// create phase using 


$(document).ready(function () {
    //$('#AddUserForm').submit(function (e) {
    //    e.preventDefault(); // <------------------ stop default behaviour of button
    $("#btnSaveproject").click(function ()
    {
        debugger;
        var projectdetails = {
            Name: $("#ProjectName").val(),

        };
        $.ajax({
            url: "/Plot/PostProject/",
            type: "POST",
            data: projectdetails,
            success: succ,
            error: err

        });
        function succ() {

            alert("Project Successfully Added");
            ClearphaseTextboxes();

        }
        function err() {
            alert("An error has occured while creating the phase");
        }

        function ClearphaseTextboxes() {
            $("#ProjectName").val("")


        }
    });
});


// end phase creating

// Load Data When click on Add Supplier




    function searchsuppplier() {
        var cnic = $('.usercnic').val();
        var indivi_Aop_company = $('.aop_company_name_id').val();
        debugger;
        $.ajax({
            type: "POST",
            data: { id: cnic, ind_aop_com: indivi_Aop_company},
            url: "/Home/Getsupplierinformation/",

            success: function (data) {
                if (data == false) {
                  
                }
                else {
                    $('.Cus_Id').val(data.Id);
                    $('.Cus_nam').val(data.Name);
                    $('.Cus_addres').val(data.Address);
                    $('.Cus_Ba').val(data.Address);
                    $('.Cus_Sa').val(data.Address);
                    $('.Cus_cni').val(data.CNIC_Number);
                    $('.Cus_Bcn').val(data.PhoneNumber);
                    $('.Cus_Scn').val(data.PhoneNumber);
                    $('.Cus_nu').val(data.PhoneNumber);
                    $('.Cus_gende').val(data.Gender).attr('disabled', true);
                    $('.Cus_nt').val(data.NTN);
                    $('.Cus_str').val(data.STRN);
                    $('.Cus_file').val(data.Filer_NonFiler).attr('disabled', true);
                    $('.Cus_in').val(data.Industry);
                    $('.Cus_re').val(data.Residential_Status).attr('disabled', true);
                    $('.Cus_bus').val(data.Business);
                    $('.Cus_SC').val(data.Supply_Chain_Role);
                    $('.Cus_IA').val(data.Individual_AOP_Company);
                    $('.Cus_emai').val(data.Email);
                    $('.Cus_Bei').val(data.Email);
                    $('.Cus_Sei').val(data.Email);
                    $('.Cus_Pt').val();
                    $('.Cus_ep').val(data.EPZ);
                    //$('.hide_add_as_supplier_link').prop("disabled", true);
  
                }
            },
            error: function () {

            },

        });
       
    }




    $(document).on("click", ".AddSupp", function ()
    {
        debugger;
        var indivi_Aop_company = $('.aop_company_name_id').val();
        var cnic = $('.usercnic').val();
        var GetValuesForSupplier =
            {

                Name: $(".Cus_nam").val(),
                Billing_Address: $(".Cus_Ba").val(),
                Shipping_Address: $(".Cus_Sa").val(),
                Billing_Contact_Number: $(".Cus_Bcn").val(),
                Shipping_Contact_Number: $(".Cus_Scn").val(),
                Billing_Email_Id: $(".Cus_Bei").val(),
                Shipping_Email_Id: $(".Cus_Sei").val(),
                Product_Type: $(".Cus_Pt").val(),
               Industry: $(".Cus_in").val(),
                CNIC_Number: $(".Cus_cni").val(),
                Supply_Chain_Role: $(".Cus_SC").val(),
                Business: $(".Cus_bus").val(),
                Payment_Method: $('.Cus_PM ').val(),
            };

        $.ajax({


            url: "/Supplier/AddSupplier/",
            type: "POST",
            data: {
                supp: GetValuesForSupplier,
                id: cnic,
                ind_aop_company: indivi_Aop_company

            },
            success: function (data) {
                if (data == false) {
                    UserAsSupplier();
                    cleartextofsetup();
                    $('.hide_add_as_supplier').hide();
                    //$('.Html_TextBox_Hidden_members_Setup_cnic').hide();
                    //$('.Html_TextBox_Hidden_members_Setup_ind_aop_comp').hide();
                 
                    $('.hide_add_as_supplier_link').hide();
                    $('.usercnic').val("");
                    $('.aop_company_name  ').val("");
                    $('.aop_company_name_id').val("");
                }
                else
                {
                    success();
                    cleartextofsetup();
                    $('.hide_add_as_supplier').hide();
                    //$('.Html_TextBox_Hidden_members_Setup_cnic').hide();
                    //$('.Html_TextBox_Hidden_members_Setup_ind_aop_comp').hide();

                    $('.hide_add_as_supplier_link').hide();
                    $('.usercnic').val("");
                    ('.aop_company_name  ').val("");
                    $('.aop_company_name_id').val("");
                }
               
            },
            error: function (data) {
                if (data == false)
                {
                    
                }
                
            }
           

        });

        function cleartextofsetup()
        {
            $('.Cus_Id').val("");
            $('.Cus_name').val("");
            $('.Cus_cnic').val("");
            $('.Cus_Ba').val("");
            $('.Cus_Sa').val("");
            $('.Cus_gender').val("");
            $('.Cus_res').val("");
            $('.Cus_ind').val("");
            $('.Cus_filer').val("");
            $('.Cus_IAP').val("");
            $('.Cus_buss').val("");
            $('.Cus_strn').val("");
            $('.Cus_ntn').val("");
            $('.Cus_SCR').val("");
            $('.Cus_epz').val("");
            $('.Cus_email').val("");
            $('.Cus_num').val("");
            $('.Cus_IA').val("");
            $('.Cus_emai').val("");
            $('.Cus_Bei').val("");
            $('.Cus_Sei').val("");
            $('.Cus_Pt').val("");
            $('.Cus_address').val(""); 
            $('.Cus_Bcn').val("");
            $('.Cus_Scn').val("");
            $('.aop_company_name').val("");
            $('.aop_company_name_id').val("");



        }


    });


// add user role in the main setup tab
var AddUserRole=1
    $(document).on("click", ".user-role-select", function () {
        AddUserRole++;
        var html = '<div id="User-' + AddUserRole + '">' +


            '<div class="row">' +
            '<div class="fa-hover col-md-10 col-sm-10 col-xs-12">' +
            '<a href="javascript:void" class="rm-userselect" data-rowindex="' + AddUserRole + '"><i class="fa fa-minus-square"></i></a>' +
            '</div>' +
            '<div class="row">' +
            ' <button onclick="searchuserinfo()" class="button-3-for-golden"> Search</button>' +
            '</div>' +
            '<div class="x_content">' +
            '<div class="form-horizontal form-label-left">' +
            '<div class="row">' +
            '<input id="Cus_Id"  type="hidden">' +
            '<div class="col-sm-3"><label >CNIC</label><div class="form-group"><input  id="" class="form-control usercnic" type="text"  ></div></div>' +
            '</div>' +
            '<div class="row">' +

            '<div class="col-sm-3"><label >Name</label><div class="form-group"><input name="Name" id="Cus_name" class="form-control" type="text" name="Name" readonly></div></div>' +
            '<div class="col-sm-3"><label >CNIC</label ><div class="form-group"><input name"CNIC_Number" id="Cus_cnic" class="form-control" type="text" name="CNIC_Number" readonly></div></div>' +
            '<div class="col-sm-3"><label >Address</label><div class="form-group"><input  id="Cus_address" class="form-control"  type="text" readonly></div></div>' +
            '<div class="col-sm-3"><label >Gender</label><div class="form-group"><select name="Cus_gender" id="Cus_gende" class="form-control" disabled><option value=""></option><option value="male">Male</option><option value="female">Female</option></select></div></div>' +
            '<div class="col-sm-3"><label >NTN</label><div class="form-group"><input name="Cus_nt" id="Cus_ntn"  class="form-control" type="text" readonly></div></div>' +
            '<div class="col-sm-3"><label >STRN</label><div class="form-group"><input name="Cus_str" id="Cus_strn" class="form-control" type="text" readonly></div></div>' +
            '<div class="col-sm-3"><label >Residentail Status</label><div class="form-group"><select name="Cus_res" id="Cus_re" class="form-control" disabled><option value=""> </option><option value="yes">Yes</option><option value="no">No</option></select></div></div >' +
            '<div class="col-sm-3"><label >Industry</label><div class="form-group"><input name="Industry" id="Cus_ind" class="form-control" type="text" readonly></div></div>' +
            '<div class="col-sm-3"><label >Filer</label ><div class="form-group"><select  id="Cus_filer" class="form-control" disabled><option value=""></option><option value="filer">Yes</option><option value="nonfiler">No</option></select></div></div >' +
            '<div class="col-sm-3"><label >Legal Status</label ><div class="form-group"><select  id="Cus_IAC" class="form-control" ><option value=""></option><option value="individual">Individual</option><option value="aop">AOP</option><option value="company">Company</option></select></div></div >' +
            '<div class="col-sm-3"><label >Business</label ><div class="form-group"><input name="Business" id="Cus_buss" class="form-control" type="text" readonly></div></div>' +
            '<div class="col-sm-3"><label >Supply Chain Role</label ><div class="form-group"><input name="Supply_Chain_Role" id="Cus_SCR" class="form-control"  type="text" readonly></div></div>' +
            '<div class="col-sm-3"><label >EPZ</label ><div class="form-group"><input  id="Cus_epz" class="form-control" type="text" readonly></div></div>' +
            '<div class="col-sm-3"><label >Email</label ><div class="form-group"><input type="text" class="form-control"  id="Cus_email" readonly></div></div>' +
            '<div class="col-sm-3"><label >Phone Number</label ><div class="form-group"><input type="text" class="form-control"  id="Cus_num" readonly></div></div>' +

            '</div>' +
            '</div>' +
            '<div>' +
            '<div class="col-sm-3"><label >Select User Role<span class="text-danger">*</span></label ><div class="form-group"><select  id="Cus_role" class="form-control" ><option value="">--Select--</option><option value="customer">Customer</option><option value="employee">Employee</option><option value="owner">Owner</option><option value="supplier">Supplier</option></select></div></div >' +
            '<div>' +
            '<div class="row">' +
            '<div><button  onclick="userRole()" class="button-grid-blue" > Submit</button ></div >' +
            '</div>'+
            '</div>' +
            '</div>';
           


        $('.all-role-form').append(html);
    });
    $(document).on("click", ".rm-userselect", function () {
        AddUserRole--;
        var id = $(this).data("rowindex");
        //var voucher_num = $("#trans-" + id + " #voucher_num").val();
        //$('#paymentcontroller tbody #t').remove();
        $("#User-" + id).remove();
    });

// end



// *****************Sweet Alerts*********************

    function success()
    {
        swal(
            'Successfully Saved!',
            '',
            'success'
        )
    }
    function FalseReturn()
    {
        swal('Somthing went wronge')
    }

    function Paidstatus()
    {
        swal('Total Amount Paid')
    }
    

   
    


   


//******************End Supplier bind table script