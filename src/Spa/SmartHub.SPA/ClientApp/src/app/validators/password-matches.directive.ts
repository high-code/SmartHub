import { Validator, FormGroup, ValidationErrors, ValidatorFn, NG_VALIDATORS, AbstractControl } from "@angular/forms";
import { Directive } from "@angular/core";
import { repeat } from "rxjs/operators";


export const passwordNotMatchValidator: ValidatorFn = (control: FormGroup): ValidationErrors | null => {
    const password = control.get('password');
    const repeatPassword = control.get('repeatedPassword');
    
    if(!password || !password.dirty)
      return null;

    if(!repeatPassword || !repeatPassword.dirty)
      return null;

    return  password.value !== repeatPassword.value ? { 'passwordNotMatch' : true } : null
 }

@Directive({
    selector: '[passwordNotMatch]',
    providers: [{ provide: NG_VALIDATORS, useExisting: PasswordNotMatchValidatorDirective, multi: true }]
})
export class PasswordNotMatchValidatorDirective implements Validator {
    
    validate(control: AbstractControl): ValidationErrors {
        return passwordNotMatchValidator(control);
    }

}