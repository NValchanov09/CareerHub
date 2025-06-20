document.addEventListener("DOMContentLoaded", function () {
    const rows = document.querySelectorAll(".link-row");

    rows.forEach(row => {
        row.addEventListener("click", function () {
            const href = this.getAttribute("data-href");
            if (href) {
                window.location.href = href;
            }
        });

        row.style.cursor = "pointer";

        row.querySelectorAll("input[type=checkbox]").forEach(checkbox => {
            checkbox.addEventListener("click", function (event) {
                event.stopPropagation();
            });
        });
    });
});