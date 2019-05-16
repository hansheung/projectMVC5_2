$("[primaryKey]").keypress(function (e) {
    var allowedChars = /^[a-z\d -]+$/i;
    var str = String.fromCharCode(e.charCode || e.which);

    var forbiddenChars = /[^a-z\d -]/gi;

    if (forbiddenChars.test(this.value)) {
        this.value = this.value.replace(forbiddenChars, '');
    }

    if (allowedChars.test(str)) {
        return true;
    }

    e.preventDefault();
    return false;
});

$("[primaryKey]").keyup(function () {
    $(this).val($(this).val().toUpperCase());
});

$("[primaryKey]").focus()

//=== Remove validation messages when a text-field is onfocus
function clearError(FieldName) {
    $("#div" + FieldName).removeClass('has-error');
    $("#error" + FieldName).html("");
};

function clearBoth(FieldName1, FieldName2) {
    $("#div" + FieldName1).removeClass('has-error');
    $("#error" + FieldName1).html("");
    $("#div" + FieldName2).removeClass('has-error');
    $("#error" + FieldName2).html("");

};

//===Allow enter to post-back form
$('.form-horizontal').on('keypress', function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode === 13) {
        $('#btnSubmit').focus(); //== Focus on button so the CSS will be correct
        $('#btnSubmit').click();
        return false;
    }
});


function isAmountKey(evt, element) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8))
        return false;
    else {
        var len = $(element).val().length;
        var index = $(element).val().indexOf('.');
        if (index > 0 && charCode == 46) {
            return false;
        }
        //if (index > 0) {
        //  var CharAfterdot = (len + 1) - index;
        //  if (CharAfterdot > 4) {
        //    return false;
        //  }
        // }

    }
    return true;
}
