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


