// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'amenity_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

AmenityResponse _$AmenityResponseFromJson(Map<String, dynamic> json) =>
    AmenityResponse()
      ..amenityId = json['amenityId'] as int?
      ..name = json['name'] as String?
      ..description = json['description'] as String?;

Map<String, dynamic> _$AmenityResponseToJson(AmenityResponse instance) =>
    <String, dynamic>{
      'amenityId': instance.amenityId,
      'name': instance.name,
      'description': instance.description,
    };
