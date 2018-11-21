//allow only decimal character..
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
$(document).ready(function () {
    $('isNumberKey').keypress(function (event) {
        return isNumberKey(event, this)
    });
});

//Allow only number
function isDigitKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) || charCode == null)
        return false;

    return true;
}
$(document).ready(function () {
    $('.isDigitKey').keypress(function (event) {
        return isDigitKey(event, this)
    });
});

//Allow only alpha:
function isAlpha(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 123) && charCode != 32)
        return false;

    return true;
}
$(document).ready(function () {

    $('.isAlpha').keypress(function (event) {
        return isAlpha(event, this)
    });
});

//validation for GSTNNO:
$(document).ready(function () {
    $(document).on('change', ".gstinnumber", function () {
        var inputvalues = $(this).val();
        var gstinformat = new RegExp('^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$');

        if (gstinformat.test(inputvalues)) {
            return true;
        } else {
            alert('Please Enter Valid GSTIN Number');
            $(".gstinnumber").val('');
            $(".gstinnumber").focus();
        }

    });
});
//validation for signotory GSTNo
//$(document).on('change', ".gstinnumbers", function () {
//    var inputvalues = $(this).val();
//    var gstinformat = new RegExp('^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$');

//    if (gstinformat.test(inputvalues)) {
//        return true;
//    } else {
//        alert('Please Enter Valid GSTIN Number');
//        $(".gstinnumbers").val('');
//        $(".gstinnumbers").focus();
//    }

//});

//Jquery for Email:
//$(document).on('change', ".validateEmail", function () {
//    var inputvalues = $(this).val();
//    var emailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

//    if (emailformat.test(inputvalues)) {
//        return true;
//    } else {
//        alert('Invalid Email Address');
//        $(".validateEmail").val('');
//        $(".validateEmail").focus();
//    }

//});
//Jquery for phone no:
$(document).on('change', ".validatephoneno", function () {
    var inputvalues = $(this).val();
    var phonenumber = /^[0]?[6789]\d{9}$/;
    if (phonenumber.test(inputvalues)) {
        return true;
    } else {
        alert('Invalid Phone Number');
        $(".validatephoneno").val('');
        $(".validatephoneno").focus();
    }

});

//Jquery for taxconsultant user
//validation for Organization name 
//$(document).ready(function () {
//    $('input[id$=txtOrganization]').bind('keyup blur', function () {
//        if (this.value.match(/[^a-zA-Z0-9 %*#@-]/g)) {
//            this.value = this.value.replace(/[^a-zA-Z0-9 %*#@-]/g, '');
//            alert("Only %,*,#,@ and alphanumeric characters are allowed..");
//        }
//    });

// Address
//$('input[id$=txtAddress]').bind('keyup blur', function () {
//    if (this.value.match(/[^a-zA-Z0-9 _/.,]/g)) {
//        this.value = this.value.replace(/[^a-zA-Z0-9 _/.,]/g, '');
//        alert("Only ,.,/,_,space and alphanumeric characters are allowed..");
//    }
//});

//GSTNUser Id
//    $('input[id$=txtGstId]').bind('keyup blur', function () {

//        if (this.value.match(/[^a-zA-Z0-9 %*#@-]/g)) {
//            this.value = this.value.replace(/[^a-zA-Z0-9 %*#@-]/g, '');
//            alert("Only %,*,#,@ and alphanumeric characters are allowed..");
//        }
//    });


//    //Signatory Orgnization Address
//    $('input[id$=txtOrgAddress]').bind('keyup blur', function () {
//        if (this.value.match(/[^a-zA-Z0-9 %*#@-]/g)) {
//            this.value = this.value.replace(/[^a-zA-Z0-9 %*#@-]/g, '');
//            alert("Only %,*,#,@ and alphanumeric characters are allowed..");
//        }
//    });
//});

//jquery for Signatory phone number:
//$(document).on('change', ".validatephonenos", function () {
//    var inputvalues = $(this).val();
//    var phonenumber = /^[0]?[6789]\d{9}$/;

//    if (phonenumber.test(inputvalues)) {
//        return true;
//    } else {
//        alert('Invalid Phone Number');
//        $(".validatephonenos").val('');
//        $(".validatephonenos").focus();
//    }

//});

// Jquery for Signatory Email:
//$(document).on('change', ".validateEmails", function () {
//    var inputvalues = $(this).val();
//    var emailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

//    if (emailformat.test(inputvalues)) {
//        return true;
//    } else {
//        alert('Invalid Email Address');
//        $(".validateEmails").val('');
//        $(".validateEmails").focus();
//    }

//});

// Jquery for alphanumeric:
$(document).on('change', ".alphanumeric", function () {
    var inputvalues = $(this).val();
    var alphanumeric = /^[0-9a-zA-Z]+$/;

    if (alphanumeric.test(inputvalues)) {
        return true;
    } else {
        alert('Only alphanumeric characters are allowed');
        $(".alphanumeric").val('');
        $(".alphanumeric").focus();
    }

});

$(document).on('change', ".alphanumerics", function () {
    var inputvalues = $(this).val();
    var alphanumeric = /^[0-9a-zA-Z]+$/;

    if (alphanumeric.test(inputvalues)) {
        return true;
    } else {
        alert('Only alphanumeric characters are allowed');
        $(".alphanumerics").val('');
        $(".alphanumerics").focus();
    }

});



//Jquery for on purchase register:
//  validation for Seller Address allowed alphanumeric and %,*,#,@ 
//$(document).ready(function () {
//    $('input[id$=txtSellerAddress]').bind('keyup blur', function () {
//        if (this.value.match(/[^a-zA-Z0-9 _/.,]/g)) {
//            this.value = this.value.replace(/[^a-zA-Z0-9 _/.,]/g, '');
//            alert("Only ,.,/,_,space and alphanumeric characters are allowed..");
//        }
//    });

// validation for consignee address allowed alphanumeric and %,*,#,@ 

//$('input[id$=txtConsigneeAddress]').bind('keyup blur', function () {
//    if (this.value.match(/[^a-zA-Z0-9 %*#@-]/g)) {
//        this.value = this.value.replace(/[^a-zA-Z0-9 %*#@-]/g, '');
//        alert("Only %,*,#,@ and alphanumeric characters are allowed..");
//    }
//});

// validation for receiver address allowed alphanumeric and  %,*,#,@ 
//$('input[id$=txtRecieverAddress]').bind('keyup blur', function () {
//    if (this.value.match(/[^a-zA-Z0-9 %*.,#@-]/g)) {
//        this.value = this.value.replace(/[^a-zA-Z0-9 %*.,#@-]/g, '');
//        alert("Only %,.,*,#,@ and alphanumeric characters are allowed..");
//    }
//});

//    $('input[id$=txtSupplierInvoiceNumber]').bind('keyup blur', function () {
//        if (this.value.match(/[^a-zA-Z0-9 _-]/g)) {
//            this.value = this.value.replace(/[^a-zA-Z0-9 _-]/g, '');
//            alert("Only _ ,- and alphanumeric characters are allowed..");
//        }
//    });
//});
//Jquery for PO number on purchase register:
//$('input[id$=txtOrderPo]').bind('keyup blur', function () {
//    if (this.value.match(/[^a-zA-Z0-9 _-]/g)) {
//        this.value = this.value.replace(/[^a-zA-Z0-9 _-]/g, '');
//        alert("Only _ ,- and alphanumeric characters are allowed..");
//    }

//});



////Jquery on Master Page
////jquery for Check null value in textbox on group:
//$(document).ready(function () {
//    $("[id*=btnGroup").bind("click", function () {

//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue<=0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//                flag = false;
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });

//});
//var parameter = Sys.WebForms.PageRequestManager.getInstance();
//parameter.add_endRequest(function () {
//    //jquery code again for working after postback
//    $("[id*=btnGroup").bind("click", function () {
//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//                flag = false;
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });

//});

////jquery for subgroup:

//$(document).ready(function () {
//    $("[id*=btnSubGroup").bind("click", function () {
//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue<=0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//                flag = false;
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });

//});
//var parameter = Sys.WebForms.PageRequestManager.getInstance();
//parameter.add_endRequest(function () {
//    //jquery code again for working after postback
//    $("[id*=btnSubGroup").bind("click", function () {
//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {
//                $(this).css({ "background-color": "#FFD2D2" });
//                flag = false;
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });

//});

////jquery for Check null value in textbox on Class
//$(document).ready(function () {
//    $("[id*=btnClass").bind("click", function () {
//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue<=0) {
//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });

//});
//var parameter = Sys.WebForms.PageRequestManager.getInstance();
//parameter.add_endRequest(function () {
//    //jquery code again for working after postback
//    $("[id*=btnClass").bind("click", function () {
//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue<=0) {
//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });

//});


////jquery for Check null value in textbox on Sub Class:

//$(document).ready(function () {
//    $("[id*=btnSubClass").bind("click", function () {
//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue<=0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });

//    });
//});

//var parameter = Sys.WebForms.PageRequestManager.getInstance();
//parameter.add_endRequest(function () {
//    //jquery code again for working after postback
//    $("[id*=btnSubClass").bind("click", function () {
//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {
//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });

//});


//// jquery for Check null value in textbox on HSN page:
//$(document).ready(function () {
//    $("[id*=btnSave").bind("click", function () {
//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= -1) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnSave").bind("click", function () {
//        $(".nullref").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnNotified").bind("click", function () {
//        $(".nullnotified").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue<=-1) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnNotified").bind("click", function () {
//        $(".nullnotifiedref").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnCondition").bind("click", function () {
//        $(".nullCondition").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });

//});
//var parameter = Sys.WebForms.PageRequestManager.getInstance();
//parameter.add_endRequest(function () {
//    //jquery code again for working after postback
//    $("[id*=btnSave").bind("click", function () {
//        $(".nullSave").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue<=-1) {
//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnNotified").bind("click", function () {
//        $(".nullnotified").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue<= -1) {
//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnNotified").bind("click", function () {
//        $(".nullnotifiedref").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <=  0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnCondition").bind("click", function () {
//        $(".nullCondition").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {
//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });

//});

//    //jquery for check null value on State Master:
//    $(document).ready(function () {
//        $("[id*=btnAddState").bind("click", function () {
//            $(".nullvalue").each(function () {
//                var txtValue = $(this).val();
//                if (txtValue.length <= 0 || txtValue<=0) {
//                    $(this).css({ "background-color": "#FFD2D2" });
//                } else {
//                    $(this).css({ "background-color": "white" });
//                }
//            });
//        });

//    });
//    var parameter = Sys.WebForms.PageRequestManager.getInstance();
//    parameter.add_endRequest(function () {
//        //jquery code again for working after postback
//        $("[id*=btnAddState").bind("click", function () {
//            $(".nullvalue").each(function () {
//                var txtValue = $(this).val();
//                if (txtValue.length <= 0 || txtValue<=0) {
//                    $(this).css({ "background-color": "#FFD2D2" });
//                } else {
//                    $(this).css({ "background-color": "white" });
//                }
//            });
//        });

//    });


////jquery for check null value on Purchase Register:

//$(document).ready(function () {
//    $("[id*=btnSaveRegister").bind("click", function () {
//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue<=0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//});
//var parameter = Sys.WebForms.PageRequestManager.getInstance();
//parameter.add_endRequest(function () {
//    //jquery code again for working after postback
//    $("[id*=btnSaveRegister").bind("click", function () {
//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue<=0) {
//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });

//});


//// jquery for check null value on vendor register:

//$(document).ready(function () {
//    $("[id*=btnRegistration").bind("click", function () {
//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= -1) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnRegistration").bind("click", function () {
//        $(".nullref").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btntransShipment").bind("click", function () {
//        $(".nullref").each(function () {
//            var txtValue = $(this).val();
//                if (txtValue.length <= 0 || txtValue <= -1) {
//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btntransShipment").bind("click", function () {
//        $(".nulltrans").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//});

//var parameter = Sys.WebForms.PageRequestManager.getInstance();
//parameter.add_endRequest(function () {
//    //jquery code again for working after postback
//    $("[id*=btnRegistration").bind("click", function () {
//        $(".nullvalue").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= -1) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnRegistration").bind("click", function () {
//        $(".nullref").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btntransShipment").bind("click", function () {
//        $(".nullrefs").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= -1) {
//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btntransShipment").bind("click", function () {
//        $(".nulltrans").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//});


//// jquery for null value for users:
//$(document).ready(function () {
//    $("[id*=btnCreateUsers").bind("click", function () {
//        $(".nullUsers").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=lkbAdd").bind("click", function () {
//        $(".nullAdd").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <=-1) {
//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//});
//var parameter = Sys.WebForms.PageRequestManager.getInstance();
//parameter.add_endRequest(function () {
//    $("[id*=btnCreateUsers").bind("click", function () {
//        $(".nullUsers").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=lkbAdd").bind("click", function () {
//        $(".nullAdd").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {
//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//});

////jquery for check null value on SAC:
//$(document).ready(function () {
//    $("[id*=btnsave").bind("click", function () {
//        $(".nullAdd").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= -1) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnsave").bind("click", function () {
//        $(".nullref").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnNotified").bind("click", function () {
//            $(".nullNotified").each(function () {
//                var txtValue = $(this).val();
//                if (txtValue.length <= 0 || txtValue < 0) {

//                    $(this).css({ "background-color": "#FFD2D2" });
//                } else {
//                    $(this).css({ "background-color": "white" });
//                }
//            });
//    });
//    $("[id*=btnNotified").bind("click", function () {
//        $(".null").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <=0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//            $("[id*=btnCondition").bind("click", function () {
//                $(".nullCondition").each(function () {
//                    var txtValue = $(this).val();
//                    if (txtValue.length <= 0 || txtValue <= 0) {

//                        $(this).css({ "background-color": "#FFD2D2" });
//                    } else {
//                        $(this).css({ "background-color": "white" });
//                    }
//                });
//            });
//});
//var parameter = Sys.WebForms.PageRequestManager.getInstance();
//parameter.add_endRequest(function () {
//    $("[id*=btnsave").bind("click", function () {
//        $(".nullAdd").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= -1) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnsave").bind("click", function () {
//        $(".nullref").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnNotified").bind("click", function () {
//        $(".nullNotified").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnNotified").bind("click", function () {
//        $(".null").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//    $("[id*=btnCondition").bind("click", function () {
//        $(".nullCondition").each(function () {
//            var txtValue = $(this).val();
//            if (txtValue.length <= 0 || txtValue <= 0) {

//                $(this).css({ "background-color": "#FFD2D2" });
//            } else {
//                $(this).css({ "background-color": "white" });
//            }
//        });
//    });
//});







