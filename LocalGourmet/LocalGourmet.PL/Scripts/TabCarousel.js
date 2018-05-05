function openPage(evt, pageName) {
    var i, x, tablinks;
    x = document.getElementsByClassName("Page");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablink");
    for (i = 0; i < x.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" btn-danger", "");
    }
    document.getElementById(pageName).style.display = "block";
    evt.currentTarget.className += " btn-danger";
}