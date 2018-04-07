document.addEventListener('DOMContentLoaded', function () {
    window.setTimeout(function () {
        if ((e = document.getElementById("answer")) !== null)
            e.className = e.className.replace(/\bincorrect\b/g, "");
    }, 500);
});