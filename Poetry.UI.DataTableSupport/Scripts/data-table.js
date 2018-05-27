/// <reference path="../../Poetry.UI.PortalSupport/Scripts/button.js"/>



/* DATA TABLE */

class DataTable {
    constructor(provider, columnNames, createColumns, createActionButton) {
        this.provider = provider;
        this.columnNames = columnNames;
        this.createColumns = createColumns;
        this.createActionButton = createActionButton;

        this.page = 1;

        this.element = document.createElement('data-table');

        this.tableOuterMinHeight = 0;
        this.tableOuter = document.createElement('div');
        this.tableOuter.classList.add('data-table-outer');
        this.element.appendChild(this.tableOuter);

        var table = document.createElement('table');
        table.classList.add('data-table');
        this.tableOuter.appendChild(table);

        var tableHeader = document.createElement('thead');
        table.appendChild(tableHeader);

        var columnHeaderRow = document.createElement('tr');
        tableHeader.appendChild(columnHeaderRow);

        columnNames.forEach(name => {
            var columnHeader = document.createElement('th');
            columnHeader.style.width = (Math.floor(100 / columnNames.length * 100) / 100) + '%';
            columnHeaderRow.appendChild(columnHeader);

            columnHeader.innerText = name;
        });

        var editColumnHeader = document.createElement('th');
        editColumnHeader.style.width = '1%';
        columnHeaderRow.appendChild(editColumnHeader);

        this.tableBody = document.createElement('tbody');
        table.appendChild(this.tableBody);

        this.paging = document.createElement('data-table-paging');
        this.element.appendChild(this.paging);

        this.update();
    }

    update() {
        fetch(`DataTable/Backend/GetAll?provider=${this.provider}&page=${this.page}`, { credentials: 'include' })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`DataTable backend returned ${response.status} (${response.statusText})`);
                }

                return response.json();
            })
            .then(response => {
                this.tableOuterMinHeight = Math.max(this.tableOuter.offsetHeight, this.tableOuterMinHeight);
                this.tableOuter.style.minHeight = this.tableOuterMinHeight + 'px';

                while (this.tableBody.lastChild) {
                    this.tableBody.removeChild(this.tableBody.lastChild);
                }

                response.Items.forEach(item => {
                    var row = document.createElement('tr');
                    this.tableBody.appendChild(row);

                    this.createColumns.forEach(callback => {
                        var cell = document.createElement('td');
                        row.appendChild(cell);

                        callback(this, cell, item);
                    });

                    var editCell = document.createElement('td');
                    editCell.classList.add('data-table-edit-column');
                    row.appendChild(editCell);

                    if (this.createActionButton) {
                        this.createActionButton(this, editCell, item);
                    }
                });

                while (this.paging.lastChild) {
                    this.paging.removeChild(this.paging.lastChild);
                }

                for (var i = 1; i <= response.PageCount; i++) {
                    (function (dataTable, page) {
                        new PortalButton(page, () => {
                            dataTable.page = page;
                            dataTable.update();
                        }).addClass('portal-button-active', page == dataTable.page).appendTo(dataTable.paging);
                    })(this, i);
                }
            });
    }

    appendTo(element) {
        element.appendChild(this.element);
    }
}