document.addEventListener("DOMContentLoaded", function() {
    // url fix after facebook login
    if (window.location.hash === "#_=_"){
        if (history.replaceState) {
            var cleanHref = window.location.href.split("#")[0];
            history.replaceState(null, null, cleanHref);
        } else {
            window.location.hash = "";
        }
    }
});