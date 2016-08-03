function setExecModeForGov() {
    window.execMode = "ForGov"; 
}

function setAutoLogin(loginStr) { 
    var loginParams = eval('(' + loginStr + ')');
    $('#Code').val(loginParams.Code);
    $('#Password').val(loginParams.Password); 
    $('#LoginButton').trigger('click');
}

function autosize(id, delta) {
    $("#" + id).height($("#" + id).parent().parent().height() + (delta || 0));
}



function loadPage(pageUrl) {
    
}