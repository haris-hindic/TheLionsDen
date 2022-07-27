// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_insert_request.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserInsertRequest _$UserInsertRequestFromJson(Map<String, dynamic> json) =>
    UserInsertRequest()
      ..firstName = json['firstName'] as String?
      ..lastName = json['lastName'] as String?
      ..username = json['username'] as String?
      ..phoneNumber = json['phoneNumber'] as String?
      ..email = json['email'] as String?
      ..password = json['password'] as String?
      ..passwordConfirmation = json['passwordConfirmation'] as String?
      ..gender = json['gender'] as String?
      ..dateOfBirth = json['dateOfBirth'] as String?
      ..roleId = json['roleId'] as int?;

Map<String, dynamic> _$UserInsertRequestToJson(UserInsertRequest instance) =>
    <String, dynamic>{
      'firstName': instance.firstName,
      'lastName': instance.lastName,
      'username': instance.username,
      'phoneNumber': instance.phoneNumber,
      'email': instance.email,
      'password': instance.password,
      'passwordConfirmation': instance.passwordConfirmation,
      'gender': instance.gender,
      'dateOfBirth': instance.dateOfBirth,
      'roleId': instance.roleId,
    };
