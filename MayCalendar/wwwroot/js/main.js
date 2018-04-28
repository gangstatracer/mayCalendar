document.addEventListener('DOMContentLoaded', function () {
    window.setTimeout(function () {
        e = document.querySelectorAll(".incorrect");
        for (var i = 0; i < e.length; i++) {
            e[i].className = e[i].className.replace(/\bincorrect\b/g, "");
        }

        validationLabels = document.getElementsByClassName("validation-error")
        while (validationLabels.length > 0) {
            validationLabels[0].parentNode.removeChild(validationLabels[0]);
        }
    }, 2000);
});