


/* APP */

class App {
    constructor(name) {
        this.name = name;
        this.blades = [];
        this.element = document.createElement('app');
    }

    openBlade(blade, parentBlade) {
        var open = () => {
            this.blades.push(blade);
            this.element.appendChild(blade.element);

            blade.element.scrollIntoView({
                behavior: 'smooth'
            });

            return new FadeInEffect(blade.element);
        }

        var bladesAfterParentBlade = parentBlade ? this.blades.slice(this.blades.indexOf(parentBlade) + 1) : [];

        if (bladesAfterParentBlade.length) {
            return this.closeBlade(bladesAfterParentBlade[0]).then(open);
        } else {
            return open();
        }
    }

    closeBlade(blade, data) {
        if (!blade) {
            throw 'No blade specified';
        }

        var index = this.blades.indexOf(blade);

        if (index == -1) {
            throw 'Blade not found';
        }

        var done;
        var promise = new Promise(resolve => done = resolve);

        this.blades.slice(index).reverse().forEach((b, i) => {
            new FadeOutEffect(b.element, i * 200).onComplete(() => {
                this.element.removeChild(b.element);
                this.blades.splice(this.blades.indexOf(b), 1);

                if (i == index) {
                    b.triggerOnClose(data);
                    done();
                } else {
                    b.triggerOnClose();
                }
            });
        });

        return promise;
    }
}
