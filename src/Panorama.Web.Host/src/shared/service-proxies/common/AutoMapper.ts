import {Constructor} from "./types";

/*
* In order for the mapper to work, DTO's must be given default values where mapping is required for blank constructions.
* */
export class AutoMapper {

    public map<TDestination>(source: any, destination: Constructor<TDestination>, arrayItemType?: Constructor<any>): TDestination {
        source = typeof source === 'object' ? source : {};

        return this._map(source, destination, arrayItemType);
    }

    public mapFromJson<TDestination>(jsonString: string, destination: Constructor<TDestination>, arrayItemType?: Constructor<any>): TDestination {
        const jsonObj = JSON.parse(jsonString);

        return this._map(jsonObj, destination, arrayItemType);
    }
    
    private _map<TDestination>(source: any, destination: Constructor<TDestination>, arrayItemType?: Constructor<any>): TDestination {
        let instance = new destination();
        
        // Loop through the keys of the instance and only assign if the param has the same key.
        for (const key in instance) {
            if (source.hasOwnProperty(key)) {

                // This worked, but then package gave issues out of the blue. Weird.
                // const metadataType = Reflect.getMetadata("design:type", destination.prototype, key);

                // if (metadataType === Array && arrayItemType) {
                //     (instance as any)[key] = source[key].map((item: any) => this._map(item, arrayItemType));
                // } else {
                //     (instance as any)[key] = source[key]; // Direct assignment for non-array properties
                // }
            }
        }

        return instance;
    }
}


