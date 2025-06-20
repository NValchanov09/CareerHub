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

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();

    var checkbox = $('table tbody input[type="checkbox"]');
    $("#selectAll").click(function () {
        if (this.checked) {
            checkbox.each(function () {
                this.checked = true;
            });
        } else {
            checkbox.each(function () {
                this.checked = false;
            });
        }
    });
    checkbox.click(function () {
        if (!this.checked) {
            $("#selectAll").prop("checked", false);
        }
    });

    $("#deleteSelected").click(function () {
        var selectedItems = [];

        $('table tbody input[name="checkbox"]:checked').each(function () {
            selectedItems.push($(this).val());
        });

        var deleteUrl = $(this).data('delete-url');

        $('.cancelDelete').click(function () {
            $('#confirmModal').modal('hide');
        });

        $('#confirmModal').on('click', function (e) {
            if ($(e.target).hasClass('modal')) {
                $(this).modal('hide');
            }
        });

        if (selectedItems.length > 0) {
            $('#confirmModal').modal('show');

            $('#confirmDelete').off('click').on('click', function () {
                $.ajax({
                    url: deleteUrl,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(selectedItems),
                    headers: {
                        'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                    },
                    success: function (response) {
                        if (response.success) {
                            window.location.reload();
                        } else {
                            alert(response.message || "Error deleting languages.");
                        }
                    },
                    error: function (xhr, status, error) {
                        alert("An error occurred: " + error);
                    }
                });

                $('#confirmModal').modal('hide');
            });
        }
    });
});