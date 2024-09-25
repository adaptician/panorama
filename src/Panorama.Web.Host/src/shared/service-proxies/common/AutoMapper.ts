import {Constructor} from "./types";

export class AutoMapper {

    public map<TDestination>(source: any, destination: Constructor<TDestination>): TDestination {
        source = typeof source === 'object' ? source : {};
        let instance = new destination();

        // Loop through the keys of the instance and only assign if the param has the same key.
        for (const key in instance) {
            if (source.hasOwnProperty(key)) {
                (instance as any)[key] = source[key]; // Cast to any to assign dynamic properties.
            }
        }
        
        return instance;
    }

    // Method to map from JSON string to the concrete type
    // public fromJson(jsonString: string): T {
    //     const jsonObj = JSON.parse(jsonString);
    //     const instance = new this.type();
    //
    //     // Loop through the keys of the instance and assign from the parsed JSON object
    //     for (const key in instance) {
    //         if (jsonObj.hasOwnProperty(key)) {
    //             (instance as any)[key] = jsonObj[key]; // Cast to any to assign dynamic properties
    //         }
    //     }
    //     return instance;
    // }
}
