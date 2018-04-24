document.addEventListener('DOMContentLoaded', function () {
    window.setTimeout(function () {
        if ((e = document.getElementById("answer")) !== null)
            e.className = e.className.replace(/\bincorrect\b/g, "");

        validationLabels = document.getElementsByClassName("validation-error")
        while (validationLabels.length > 0) {
            validationLabels[0].parentNode.removeChild(validationLabels[0]);
        }
    }, 2000);
});