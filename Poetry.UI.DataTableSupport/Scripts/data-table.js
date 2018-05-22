


/* DATA TABLE */

class DataTable {
    constructor(provider, columnNames, createColumns, createActionButton) {
        this.provider = provider;
        this.columnNames = columnNames;
        this.createColumns = createColumns;
        this.createActionButton = createActionButton;

        this.page = 1;

        this.root = document.createElement('data-table');

        var table = document.createElement('table');
        table.classList.add('data-table');
        this.root.appendChild(table);

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
        this.root.appendChild(this.paging);

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
                    var page = document.createElement('a');
                    page.classList.add('portal-small-button');
                    page.innerText = i;
                    (function (dataTable, i) {
                        page.addEventListener('click', () => {
                            dataTable.page = i;
                            dataTable.update();
                        });
                    })(this, i);
                    this.paging.appendChild(page);
                }
            });
    }

    appendTo(element) {
        element.appendChild(this.root);
    }
}