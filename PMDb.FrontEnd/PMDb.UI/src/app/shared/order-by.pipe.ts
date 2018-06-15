import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'orderBy'
})
export class OrderByPipe implements PipeTransform {

  transform(value: any, [config = '+']): any {

//     if(!Array.isArray(value)) return value; //value isn't even an array can't sort

//     //if(!Array.isArray(config) || (Array.isArray(config) && config.length == 1)){    
//       //Single property to sort on
//       var propertyToCheck:string = !Array.isArray(config) ? config : config[0];
//       var desc = propertyToCheck.substr(0, 1) == '-';

//       if(!propertyToCheck || propertyToCheck == '-' || propertyToCheck == '+'){
//         //is a basic array that is sorting on the array's object itself
//         return !desc ? value.sort() : value.sort().reverse();
//       }
//       //else {
//         //is a complex array that is sorting on a single property
//      // }
//   //  }
//   //  else {
//       //is a complex array with multiple properties to sort on
// //}

  }

}
