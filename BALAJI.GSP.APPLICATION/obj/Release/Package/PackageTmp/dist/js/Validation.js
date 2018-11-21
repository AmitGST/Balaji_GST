$(document).ready(function (e) {
    $("[id*=btnCreateUsers]").bind("click", function () {
       alert("The paragraph was clicked.");
        var isValid = false;
        var regex = new RegExp('/^[a-zA-Z0-9%*# @-]*$/');
        isValid = regex.test($("[id*=txtOrganization]").val());
        $("#spnError2").css("display", !isValid ? "block" : "none");
        return isValid;
    });
    $("[id*=btnCreateUsers]").bind("click", function () {
        var isValid = false;
        var regex = new RegExp('/^[[a-zA-Z0-9%*#@-]*$/');
        isValid = regex.test($("[id*=txtGstId]").val());
        $("#spnError3").css("display", !isValid ? "block" : "none");
        return isValid;
    });
    $("[id*=btnCreateUsers]").bind("click", function () {
        alert(regex);
        var isValid = false;
        var regex = new RegExp('/^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/');
        isValid = regex.test($("[id*=txtGSTNNo]").val());
        $("#spnError4").css("display", !isValid ? "block" : "none");
        return isValid;
    });
    $("[id*=btnCreateUsers]").bind("click", function () {
        //alert("The EMail was clicked.");
        var isValid = false;
        var regex = new RegExp('/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/');
        isValid = regex.test($("[id*=txtEmailID]").val());
        $("#spnError5").css("display", !isValid ? "block" : "none");
        return isValid;
    });
    $("[id*=btnCreateUsers]").bind("click", function () {
        //alert("The phone was clicked.");
        var isValid = false;
        var regex = new RegExp('/^[0]?[6789]\d{9}$/');
        isValid = regex.test($("[id*=txtPhoneNo]").val());
        $("#spnError6").css("display", !isValid ? "block" : "none");
        return isValid;
    });
});