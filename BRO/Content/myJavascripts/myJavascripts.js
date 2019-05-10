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

//=== Show and unshow PASSWORD
$(".toggle-PASSWORD").click(function () {
    //=== Proper way of doing toggleClass for glyphicon and font-awesome
    $(this).toggleClass('glyphicon-eye-close', 'remove');
    $(this).toggleClass('glyphicon-eye-open', 'add');
    //=================================================================
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "PASSWORD") {
        input.attr("type", "text");
    } else {
        input.attr("type", "PASSWORD");
    }
});
