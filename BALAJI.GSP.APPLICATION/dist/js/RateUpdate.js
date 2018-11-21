
$(function () {
    disableall();
});
$('html').on('change', function () {
    disableall();
});
$('html').on('click', function () { disableall(); });
window.onload = function(){
    disableall();
};



function txtDecimalFocus(e)
{
    var amount = parseFloat($(e).val());
    if(amount==NaN||amount<=0)
    {
        $(e).val('');
    }
}

function txtDecimalBlur(e)
{
    var amount = parseFloat($(e).val());
    if(amount== NaN || amount <=0)
    {
        $(e).val('0.0');
    }
}

function disableall() {
        $('#MainContent_ucExportGstr1Add tr td:nth-child(3)').each(function () {
            var ttamount = parseFloat($(this).find('input').val());
            if (ttamount <= 0) {
                $(this).next('td').find('input').attr('disabled', 'true');
                //$(this).next('td').next('td').find('input').attr('disabled', 'true');
            }
        });
        $('#MainContent_uccrdrregGSTR1 tr td:nth-child(3)').each(function () {
            var ttamount = parseFloat($(this).find('input').val());
            if (ttamount <= 0) {
                $(this).next('td').find('input').attr('disabled', 'true');
                $(this).next('td').next('td').find('input').attr('disabled', 'true');
            }
        });
        $('#MainContent_Credit_Gstr1 tr td:nth-child(3)').each(function () {
            var ttamount = parseFloat($(this).find('input').val());
            if (ttamount <= 0) {
                $(this).next('td').find('input').attr('disabled', 'true');
                $(this).next('td').next('td').find('input').attr('disabled', 'true');
            }
        });
        $('#MainContent_AdvanceGSTR1 tr td:nth-child(3)').each(function () {
            var ttamount = parseFloat($(this).find('input').val());
            if (ttamount <= 0) {
                $(this).next('td').find('input').attr('disabled', 'true');
                $(this).next('td').next('td').find('input').attr('disabled', 'true');
            }
        });
        $('#MainContent_B2CLGSTR1 tr td:nth-child(3)').each(function () {
            var ttamount = parseFloat($(this).find('input').val());
            if (ttamount <= 0) {
                $(this).next('td').find('input').attr('disabled', 'true');
                $(this).next('td').next('td').find('input').attr('disabled', 'true');
            }
        });
    }


    function calculateIGSTB2CL(e) {
        var amount = parseFloat($(e).val());
        if (amount > 0) {
            var rate = parseFloat($(e).parent('td').prev('td').text().split(' %')[0]);
            if (rate > 0) {
                var igstrateamount = (amount * (rate / 100)).toFixed(2);
            }
            else {
                igstrateamount = amount;
            }
            $(e).parent('td').next('td').find('input').val(igstrateamount);
            $(e).parent('td').next('td').find('input').removeAttr('disabled');
            $(e).parent('td').next('td').next('td').find('input').removeAttr('disabled');
        }
        else {
            //$(e).val("0.0");
            $(e).parent('td').next('td').find('input').val("0.0");
            $(e).parent('td').next('td').next('td').find('input').val("0.0");
            $(e).parent('td').next('td').find('input').attr('disabled', 'true');
            $(e).parent('td').next('td').next('td').find('input').attr('disabled', 'true');
        }
    }
    function calculateIGSTCRDR(e) {
        var amount = parseFloat($(e).val());
        if (amount > 0) {
            var rate = parseFloat($(e).parent('td').prev('td').find('span').text().split(' %')[0]);
            if (rate > 0) {
                var igstrateamount = (amount * (rate / 100)).toFixed(2);
            }
            else {
                igstrateamount = amount;
            }
            $(e).parent('td').next('td').find('input').val(igstrateamount);
            $(e).parent('td').next('td').find('input').removeAttr('disabled');
            $(e).parent('td').next('td').next('td').find('input').removeAttr('disabled');
        }
        else {
            //$(e).val("0.0");
            $(e).parent('td').next('td').find('input').val("0.0");
            $(e).parent('td').next('td').next('td').find('input').val("0.0");
            $(e).parent('td').next('td').find('input').attr('disabled', 'true');
            $(e).parent('td').next('td').next('td').find('input').attr('disabled', 'true');
        }
    }
    function calculateIGSTEXP(e) {
        var amount = parseFloat($(e).val());
        if (amount > 0) {
            var rate = parseFloat($(e).parent('td').prev('td').find('span').text().split(' %')[0]);
            if (rate > 0) {
                var igstrateamount = (amount * (rate / 100)).toFixed(2);
            }
            else {
                igstrateamount = amount;
            }
            $(e).parent('td').next('td').find('input').val(igstrateamount);
            $(e).parent('td').next('td').find('input').removeAttr('disabled');
            //$(e).parent('td').next('td').next('td').find('input').removeAttr('disabled');
        }
        else {
            //$(e).val("0.0");
            $(e).parent('td').next('td').find('input').val("0.0");
            $(e).parent('td').next('td').next('td').find('input').val("0.0");
            $(e).parent('td').next('td').find('input').attr('disabled', 'true');
            //$(e).parent('td').next('td').next('td').find('input').attr('disabled', 'true');
        }
    }
    function calculateIGSTAdv(e) {
        var amount = parseFloat($(e).val());
        if (amount > 0) {
            var rate = parseFloat($(e).parent('td').prev('td').find('span').text().split(' %')[0]);
            if (rate > 0) {
                var igstrateamount = (amount * (rate / 100)).toFixed(2);
            }
            else {
                igstrateamount = amount;
            }
            $(e).parent('td').next('td').find('input').val(igstrateamount);
            $(e).parent('td').next('td').find('input').removeAttr('disabled');
            $(e).parent('td').next('td').next('td').find('input').removeAttr('disabled');
        }
        else {
            //$(e).val("0.0");
            $(e).parent('td').next('td').find('input').val("0.0");
            $(e).parent('td').next('td').next('td').find('input').val("0.0");
            $(e).parent('td').next('td').find('input').attr('disabled', 'true');
            $(e).parent('td').next('td').next('td').find('input').attr('disabled', 'true');
        }
    }
