import {Constructor} from "./types";

export class AutoMapper {

    public map<TDestination>(source: any, destination: Constructor<TDestination>): TDestination {
        source = typeof source === 'object' ? source : {};

        return this._map(source, destination);
    }

    public mapFromJson<TDestination>(jsonString: string, destination: Constructor<TDestination>): TDestination {
        const jsonObj = JSON.parse(jsonString);

        return this._map(jsonObj, destination);
    }
    
    private _map<TDestination>(source: any, destination: Constructor<TDestination>): TDestination {
        let instance = new destination();
        
        // Loop through the keys of the instance and only assign if the param has the same key.
        for (const key in instance) {
            if (source.hasOwnProperty(key)) {
                (instance as any)[key] = source[key]; // Cast to any to assign dynamic properties.
            }
        }

        return instance;
    }
}
