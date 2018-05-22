


/* TRANSLATION */

class Translation {
    constructor(map, scope) {
        this.map = map || {};
        this.scope = scope;
    }

    add(id, translations) {
        this.map[id] = translations;
    }

    get(path) {
        var segments = path.split('/');
        var scope = this.scope || segments[0];
        var translations = this.map[scope];
        var target = translations;

        for (var i = this.scope ? 0 : 1; i < segments.length; i++) {
            if (!target) {
                return;
            }

            target = target[segments[i]];
        }

        return target;
    }

    scopeTo(scope) {
        return new Translation(this.map, scope);
    }
}

var translations = new Translation();