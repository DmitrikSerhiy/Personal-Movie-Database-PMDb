import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'customeDecimalPipe'
})
export class CustomeDecimalPipePipe implements PipeTransform {

  transform(value: number, returnFractionDigit : boolean ): number {
    var derivedValue = value.toString().split('.');
    if(returnFractionDigit){
      var fractionDigit = derivedValue.pop();
      if(derivedValue.length === 0)
        return 0;
      if(fractionDigit.length > 1)
        fractionDigit = fractionDigit.charAt(0);
    
        var fNew = +fractionDigit
        return fNew;
      }
    else
      return +derivedValue[0]
  }
}
