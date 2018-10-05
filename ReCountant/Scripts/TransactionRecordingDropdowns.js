
                            //function Getname(e) {
                            //    $.ajax({
                            //        traditional: true,
                            //        type: "POST",
                            //        data: { s_name: e },
                            //        url: "/Supplier/GetSname/",
                            //        success: function (data) {
                            //            $('#allsup_name').empty();
                            //            //$('#allsup_cnic').append('<option value="">Select CNIC</option>');
                            //            $.each(data, function (key, value) {
                            //                $('#allsup_name').append('<option value=' + value.Id + '>' + value.name + '</option>');
                            //            });

                            //        },
                            //        error: function () {

                            //        }
                            //    });

                            //}
                            //$('#suggestions').autocomplete({ source: get_suggestions, appendTo: '#my-suggestions' })



                        // search vouvher type(purchase and payables, transaction recording)
                          
                        // when select all get report of all 
                             $('#SupplierGridOption').change(function () {
                                 var selectedoption = $('#SupplierGridOption option:selected')

                                 if (selectedoption.val() == "allsupplier") {
                                     debugger;
                                     BindTableForSupplier();
                                 }

                             });
                        // report all table function
                             function BindTableForSupplier() {
                                 debugger;

                                 $.ajax({
                                     type: "POST",
                                     url: "/Supplier/GetAndPrintSupplier/",
                                     data: "{}",
                                     contentType: "application/json; charset=utf-8",
                                     dataType: "json",
                                     success: function (data) {

                                         var items = '';
                                         var rows = "<tr>" +
                                             "<th align='left' class='SupplierTableTH'>Project</th><th align='left' class='SupplierTableTH'>Transaction Party</th><th align='left' class='SupplierTableTH'>Voucher Type</th>" +
                                             "<th align='left' class='SupplierTableTH'>Voucher Number</th><th align='left' class='SupplierTableTH'>Document Date</th><th align='left' class='SupplierTableTH'>Document Date Of Entry</th>" +
                                             "<th align='left' class='SupplierTableTH'>Description</th><th align='left' class='SupplierTableTH'>Space Type</th><th align='left' class='SupplierTableTH'>Plot</th>" +
                                             "<th align='left' class='SupplierTableTH'>Land</th><th align='left' class='SupplierTableTH'>Quantity</th><th align='left' class='SupplierTableTH'>Amount (LC)</th>" +

                                             "</tr>";
                                         $('#tblSupplier tbody').append(rows);

                                         $.each(data, function (i, item) {
                                             var rows = "<tr class='SupplierTableTD'>" +
                                                 "<td style='border: 1px solid black;'>" + item.Project + "</td>" +
                                                 "<td style='border: 1px solid black;'>" + item.Transaction_Party + "</td>" +
                                                 "<td style='border: 1px solid black;'>" + item.Voucher_Type + "</td>" +
                                                 "<td style='border: 1px solid black;'>" + item.voucher_number + "</td>" +
                                                 "<td style='border: 1px solid black;'>" + item.document_Date + "</td>" +
                                                 "<td style='border: 1px solid black;'>" + item.dateTime_Of_Entry + "</td>" +
                                                 "<td style='border: 1px solid black;'>" + item.Description + "</td>" +
                                                 "<td style='border: 1px solid black;'>" + item.Space_Type + "</td>" +
                                                 "<td style='border: 1px solid black;'>" + item.Plot + "</td>" +
                                                 "<td style='border: 1px solid black;'>" + item.Land + "</td>" +
                                                 "<td style='border: 1px solid black;'>" + item.Quantity + "</td>" +
                                                 "<td style='border: 1px solid black;'>" + item.Amount_LC + "</td>" +
                                                 "</tr>";
                                             $('#tblSupplier tbody').append(rows);

                                         });
                                     },

                                 });


                             }
                        // get report with all conditions
                             //function Gen_Rep_With_Vali() {
                             //    debugger;

                             //    var sup_U_Id = $('#supp_unique_code').val();
                             //    var sVocTy = $('#Supplier_VT').val();
                             //    var fdate = $('#fromdatereport').val();
                             //    var todate = $('#todatereport').val();
                             //    $.ajax({
                             //        type: "POST",
                             //        url: "/Supplier/GetReportWIthNameNAndCnic/",
                             //        data: {
                             //            VT: sVocTy,
                             //            Supplier_Unique_Id: sup_U_Id,
                             //            fromdate: fdate,
                             //            todate: todate
                             //        },

                             //        success: function (data) {

                             //            var items = '';
                             //            var rows = "<tr>" +
                             //                "<th align='left' class='SupplierTableTH'>Project</th><th align='left' class='SupplierTableTH'>Transaction Party</th><th align='left' class='SupplierTableTH'>Voucher Type</th>" +
                             //                "<th align='left' class='SupplierTableTH'>Voucher Number</th><th align='left' class='SupplierTableTH'>Document Date</th><th align='left' class='SupplierTableTH'>Document Date Of Entry</th>" +
                             //                "<th align='left' class='SupplierTableTH'>Description</th><th align='left' class='SupplierTableTH'>Space Type</th><th align='left' class='SupplierTableTH'>Plot</th>" +
                             //                "<th align='left' class='SupplierTableTH'>Land</th><th align='left' class='SupplierTableTH'>Quantity</th><th align='left' class='SupplierTableTH'>Amount (LC)</th>" +

                             //                "</tr>";
                             //            $('#tblSupplier tbody').append(rows);

                             //            $.each(data, function (i, item) {
                             //                var rows = "<tr class='SupplierTableTD'>" +
                             //                    "<td style='border: 1px solid black;'>" + item.Project + "</td>" +
                             //                    "<td style='border: 1px solid black;'>" + item.Transaction_Party + "</td>" +
                             //                    "<td style='border: 1px solid black;'>" + item.Voucher_Type + "</td>" +
                             //                    "<td style='border: 1px solid black;'>" + item.voucher_number + "</td>" +
                             //                    "<td style='border: 1px solid black;'>" + item.document_Date + "</td>" +
                             //                    "<td style='border: 1px solid black;'>" + item.dateTime_Of_Entry + "</td>" +
                             //                    "<td style='border: 1px solid black;'>" + item.Description + "</td>" +
                             //                    "<td style='border: 1px solid black;'>" + item.Space_Type + "</td>" +
                             //                    "<td style='border: 1px solid black;'>" + item.Plot + "</td>" +
                             //                    "<td style='border: 1px solid black;'>" + item.Land + "</td>" +
                             //                    "<td style='border: 1px solid black;'>" + item.Quantity + "</td>" +
                             //                    "<td style='border: 1px solid black;'>" + item.Amount_LC + "</td>" +
                             //                    "</tr>";
                             //                $('#tblSupplier tbody').append(rows);
                             //            });
                             //            ClearTextBoxOfSearchFeild();
                             //        },

                             //    });
                             //    function ClearTextBoxOfSearchFeild() {
                             //       $('#supp_unique_code').val("");
                             //        $('#Supplier_VT').val("");
                             //        $('#fromdatereport').val("");
                             //        $('#todatereport').val("");
                             //    }


                             //}

                            //function from_date_to_date() {
                            //    debugger;

                            //    var fdate = $('#fromdatereport').val();
                            //    var todate = $('#todatereport').val();
                            //    $.ajax({
                            //        type: "POST",
                            //        url: "/Supplier/DateFilter/",
                            //        data: {
                            //            fromdate: fdate,
                            //            todate: todate
                            //        },

                            //        success: function (data) {

                            //            var items = '';
                            //            var rows = "<tr>" +
                            //                "<th align='left' class='SupplierTableTH'>Project</th><th align='left' class='SupplierTableTH'>Transaction Party</th><th align='left' class='SupplierTableTH'>Voucher Type</th>" +
                            //                "<th align='left' class='SupplierTableTH'>Voucher Number</th><th align='left' class='SupplierTableTH'>Document Date</th><th align='left' class='SupplierTableTH'>Document Date Of Entry</th>" +
                            //                "<th align='left' class='SupplierTableTH'>Description</th><th align='left' class='SupplierTableTH'>Space Type</th><th align='left' class='SupplierTableTH'>Plot</th>" +
                            //                "<th align='left' class='SupplierTableTH'>Land</th><th align='left' class='SupplierTableTH'>Quantity</th><th align='left' class='SupplierTableTH'>Amount (LC)</th>" +

                            //                "</tr>";
                            //            $('#tblSupplier tbody').append(rows);

                            //            $.each(data, function (i, item) {
                            //                var rows = "<tr class='SupplierTableTD'>" +
                            //                    "<td style='border: 1px solid black;'>" + item.Project + "</td>" +
                            //                    "<td style='border: 1px solid black;'>" + item.Transaction_Party + "</td>" +
                            //                    "<td style='border: 1px solid black;'>" + item.Voucher_Type + "</td>" +
                            //                    "<td style='border: 1px solid black;'>" + item.voucher_number + "</td>" +
                            //                    "<td style='border: 1px solid black;'>" + item.document_Date + "</td>" +
                            //                    "<td style='border: 1px solid black;'>" + item.dateTime_Of_Entry + "</td>" +
                            //                    "<td style='border: 1px solid black;'>" + item.Description + "</td>" +
                            //                    "<td style='border: 1px solid black;'>" + item.Space_Type + "</td>" +
                            //                    "<td style='border: 1px solid black;'>" + item.Plot + "</td>" +
                            //                    "<td style='border: 1px solid black;'>" + item.Land + "</td>" +
                            //                    "<td style='border: 1px solid black;'>" + item.Quantity + "</td>" +
                            //                    "<td style='border: 1px solid black;'>" + item.Amount_LC + "</td>" +
                            //                    "</tr>";
                            //                $('#tblSupplier tbody').append(rows);
                            //            });
                            //        },

                            //    });


                            //}
                 





  //transaction recording dropdownns main forms

  

                 
                   



                

                   

                 

                  

                   
                    
                   